using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrolAPI.Repository;
using PayrolAPI.Repository.IRepository;
using SharedModels;
using SharedModels.Dto;

namespace PayrolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IIncomeRepository _incomeRepo;
        private readonly ILogger<IncomeController> _logger;
        private readonly IMapper _mapper;
        public IncomeController(IEmployeeRepository employeeRepo, IIncomeRepository incomeRepo, ILogger<IncomeController> logger, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _incomeRepo = incomeRepo;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<IncomeDto>>> GetIncomes()
        {
            try
            {
                _logger.LogInformation("Obteniendo los ingresos");

                var incomes = await _incomeRepo.GetAllAsync();

                return Ok(_mapper.Map<IEnumerable<IncomeDto>>(incomes));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los ingresos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener los ingresos");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IncomeDto>> GetIncome(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"ID de ingresos no válida: {id}");
                return BadRequest("ID de ingresos no válida");
            }

            try
            {
                _logger.LogInformation($"Obteniendo ingresos con ID: {id}");

                var income = await _incomeRepo.GetByIdAsync(id);

                if (income == null)
                {
                    _logger.LogWarning($"No se encontró ningun ingresos con ID: {id}");
                    return NotFound("Ingreso no encontrado.");
                }

                return Ok(_mapper.Map<IncomeDto>(income));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener ingresos con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener los ingresos.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IncomeDto>> PostIncomes(IncomeCreateDto createDto)
        {
            if (createDto == null)
            {
                _logger.LogError("Se recibió un ingrersos nulo en la solicitud.");
                return BadRequest("El ingreso no puede ser nulo.");
            }

            try
            {
                _logger.LogInformation($"Creando un nuevo ingreso para el empleado con " +
                    $"ID: {createDto.EmployeeID} ");

                var employeeExists = await _employeeRepo
                    .ExistsAsync(s => s.EmployeeID == createDto.EmployeeID);

                if (!employeeExists)
                {
                    _logger.LogWarning($"El Empleado con ID '{createDto.EmployeeID}' no existe.");
                    ModelState.AddModelError("EmpleadoNoExiste", "¡El empleado no existe!");
                    return BadRequest(ModelState);
                }

                var existingIncome = await _incomeRepo
                    .GetAsync(a => a.EmployeeID == createDto.EmployeeID
                    );

                if (existingIncome != null)
                {
                    _logger.LogWarning($"Los ingresos para el empleado con ID " +
                        $"'{createDto.EmployeeID}'");
                    ModelState.AddModelError("IngresoExiste",
                        "¡Los ingresos para este empleado ya existe!");
                    return BadRequest(ModelState);
                }

           
                if (!ModelState.IsValid)
                {
                    _logger.LogError("El modelo de ingresos no es válido.");
                    return BadRequest(ModelState);
                }

           
                var newIncome = _mapper.Map<Income>(createDto);

                await _incomeRepo.CreateAsync(newIncome);

                _logger.LogInformation($"Nuevo ingreso creado con ID: " +
                    $"{newIncome.IncomeId}");
                return CreatedAtAction(nameof(GetIncome),
                    new { id = newIncome.IncomeId }, newIncome);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear un nuevo ingreso: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al crear un nuevo ingreso.");
            }
        }
        [HttpGet("CalculateTotalIncome/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CalculateTotalIncome(int employeeId)
        {
            var employeeExists = await _employeeRepo
                   .ExistsAsync(s => s.EmployeeID == employeeId);

            if (!employeeExists)
            {
                _logger.LogWarning($"El Empleado con ID '{employeeId}' no existe.");
                ModelState.AddModelError("EmpleadoNoExiste", "¡El empleado no existe!");
                return BadRequest(ModelState);
            }
            try
            {
                var totalIncome = await _incomeRepo.CalculateTotalIncomeAsync(employeeId);
                return Ok(totalIncome);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al calcular el salario bruto: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutIncomes(int id,
            IncomeUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.IncomeId)
            {
                return BadRequest("Los datos de entrada no son válidos " +
                    "o el ID de los ingresos no coincide.");
            }

            try
            {
                _logger.LogInformation($"Actualizando ingresos con ID: {id}");

                var existingIncome = await _incomeRepo.GetByIdAsync(id);
                if (existingIncome == null)
                {
                    _logger.LogWarning($"No se encontró ningun ingreso con ID: {id}");
                    return NotFound("El ingreso no existe.");
                }

          
                var employeeExists = await _employeeRepo
                    .ExistsAsync(s => s.EmployeeID == updateDto.EmployeeID);

                if (!employeeExists)
                {
                    _logger.LogWarning($"El empleado con ID '{updateDto.EmployeeID}' no existe.");
                    ModelState.AddModelError("EmpleadoNoExiste", "¡El empleado no existe!");
                    return BadRequest(ModelState);
                }

                _mapper.Map(updateDto, existingIncome);

                await _incomeRepo.SaveChangesAsync();

                _logger.LogInformation($"Ingresos con ID {id} actualizados correctamente.");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await _incomeRepo.ExistsAsync(a => a.IncomeId == id))
                {
                    _logger.LogWarning($"No se encontró ningun ingreso con ID: {id}");
                    return NotFound("EL ingreso no se encontró durante la actualización.");
                }
                else
                {
                    _logger.LogError($"Error de concurrencia al actualizar los ingresos " +
                        $"con ID: {id}. Detalles: {ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error interno del servidor al actualizar los ingresos.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar los ingresos con ID {id}: " +
                    $"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al actualizar los ingresos.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteIncomes(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando ingresos con ID: {id}");

                var income = await _incomeRepo.GetByIdAsync(id);
                if (income == null)
                {
                    _logger.LogWarning($"No se encontró ningun ingreso con ID: {id}");
                    return NotFound("Ingreso no encontrada.");
                }

                await _incomeRepo.DeleteAsync(income);

                _logger.LogInformation($"Ingreso con ID {id} eliminado correctamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el ingreso con ID {id}: " +
                    $"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Se produjo un error al eliminar el ingreso.");
            }
        }
    
    }
}
