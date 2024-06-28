using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrolAPI.Repository.IRepository;
using SharedModels.Dto;
using SharedModels;
using PayrolAPI.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace PayrolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeductionController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IDeductionRepository _deductionRepo
            ;
        private readonly ILogger<IncomeController> _logger;
        private readonly IMapper _mapper;
        public DeductionController(IEmployeeRepository employeeRepo, IDeductionRepository deductionRepo, ILogger<IncomeController> logger, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
           _deductionRepo = deductionRepo;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DeductionDto>>> GetDeduction()
        {
            try
            {
                _logger.LogInformation("Obteniendo los egresos");

                var deductions = await _deductionRepo.GetAllAsync();

                return Ok(_mapper.Map<IEnumerable<DeductionDto>>(deductions));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los egresos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener los egresos");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeductionDto>> GeteDeduction(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"ID de egresos no válida: {id}");
                return BadRequest("ID de egresos no válida");
            }

            try
            {
                _logger.LogInformation($"Obteniendo egresos con ID: {id}");

                var deduction = await _deductionRepo.GetByIdAsync(id);

                if (deduction == null)
                {
                    _logger.LogWarning($"No se encontró ningun egreso con ID: {id}");
                    return NotFound("Egreso no encontrado.");
                }

                return Ok(_mapper.Map<DeductionDto>(deduction));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener egresos con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener los egresos.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeductionDto>> PostDeduction(DeductionCreateDto createDto)
        {
            if (createDto == null)
            {
                _logger.LogError("Se recibió un egreso nulo en la solicitud.");
                return BadRequest("El egreso no puede ser nulo.");
            }

            try
            {
                _logger.LogInformation($"Creando un nuevo egreso para el empleado con " +
                    $"ID: {createDto.EmployeeID} ");

                var employeeExists = await _employeeRepo
                    .ExistsAsync(s => s.EmployeeID == createDto.EmployeeID);

                if (!employeeExists)
                {
                    _logger.LogWarning($"El Empleado con ID '{createDto.EmployeeID}' no existe.");
                    ModelState.AddModelError("EmpleadoNoExiste", "¡El empleado no existe!");
                    return BadRequest(ModelState);
                }

                var existingDeduction = await _deductionRepo
                    .GetAsync(a => a.EmployeeID == createDto.EmployeeID
                    );

                if (existingDeduction != null)
                {
                    _logger.LogWarning($"Los egresos para el empleado con ID " +
                        $"'{createDto.EmployeeID}'");
                    ModelState.AddModelError("EgresoExiste",
                        "¡Los egresos para este empleado ya existe!");
                    return BadRequest(ModelState);
                }


                if (!ModelState.IsValid)
                {
                    _logger.LogError("El modelo de egresos no es válido.");
                    return BadRequest(ModelState);
                }


                var newDeduction = _mapper.Map<Deduction>(createDto);

                await _deductionRepo.CreateAsync(newDeduction);

                _logger.LogInformation($"Nuevo egreso creado con ID: " +
                    $"{newDeduction.DeductionID}");
                return CreatedAtAction(nameof(GeteDeduction),
                    new { id = newDeduction.DeductionID }, newDeduction);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear un nuevo egreso: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al crear un nuevo egreso.");
            }
        }
        [HttpGet("CalculateTotalDeduction/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CalculateDeductions(int employeeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employeeExists = await _employeeRepo
                    .ExistsAsync(s => s.EmployeeID ==employeeId);

            if (!employeeExists)
            {
                _logger.LogWarning($"El Empleado con ID '{employeeId}' no existe.");
                ModelState.AddModelError("EmpleadoNoExiste", "¡El empleado no existe!");
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _deductionRepo.CalculateTotalDeductionAsync(employeeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al calcular deducciones: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutDeduction(int id,
            DeductionUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.DeductionID)
            {
                return BadRequest("Los datos de entrada no son válidos " +
                    "o el ID de los egresos no coincide.");
            }

            try
            {
                _logger.LogInformation($"Actualizando egresos con ID: {id}");

                var existingDeduction = await _deductionRepo.GetByIdAsync(id);
                if (existingDeduction == null)
                {
                    _logger.LogWarning($"No se encontró ningun egreso con ID: {id}");
                    return NotFound("El egreso no existe.");
                }


                var employeeExists = await _employeeRepo
                    .ExistsAsync(s => s.EmployeeID == updateDto.EmployeeID);

                if (!employeeExists)
                {
                    _logger.LogWarning($"El empleado con ID '{updateDto.EmployeeID}' no existe.");
                    ModelState.AddModelError("EmpleadoNoExiste", "¡El empleado no existe!");
                    return BadRequest(ModelState);
                }

                _mapper.Map(updateDto, existingDeduction);

                await _deductionRepo.SaveChangesAsync();

                _logger.LogInformation($"Egresos con ID {id} actualizados correctamente.");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await _deductionRepo.ExistsAsync(a => a.DeductionID == id))
                {
                    _logger.LogWarning($"No se encontró ningun egreso con ID: {id}");
                    return NotFound("EL egreso no se encontró durante la actualización.");
                }
                else
                {
                    _logger.LogError($"Error de concurrencia al actualizar los egresos " +
                        $"con ID: {id}. Detalles: {ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error interno del servidor al actualizar los egresos.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar los egresos con ID {id}: " +
                    $"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al actualizar los egresos.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDeduction(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando egreso con ID: {id}");

                var deduction = await _deductionRepo.GetByIdAsync(id);
                if (deduction == null)
                {
                    _logger.LogWarning($"No se encontró ningun egreso con ID: {id}");
                    return NotFound("Egreso no encontrado.");
                }

                await _deductionRepo.DeleteAsync(deduction);

                _logger.LogInformation($"Egreso con ID {id} eliminado correctamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el egreso con ID {id}: " +
                    $"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Se produjo un error al eliminar el egreso.");
            }
        }

    }
}
