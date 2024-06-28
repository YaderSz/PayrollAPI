using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrolAPI.Repository.IRepository;
using SharedModels.Dto;
using SharedModels;

namespace PayrolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
       
        private readonly IPayrollRepository _payrollRepo;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        public PayrollController( IPayrollRepository payrollRepo, ILogger<EmployeeController> logger, IMapper mapper)
        {
           
            _payrollRepo = payrollRepo;
            _logger = logger;

            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PayrollDto>>> GetPayroll()
        {
            try
            {
                _logger.LogInformation("Obteniendo las nominas");
                var payrolls = await _payrollRepo.GetAllAsync();

                return Ok(_mapper.Map<IEnumerable<PayrollDto>>(payrolls));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener a las nominas: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener las nominas");

            }
        }

        [HttpGet("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PayrollDto>> GetPayroll(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"ID de la nomina no es válido: {id}");
                return BadRequest("ID de la nomina no es válido");
            }

            try
            {
                _logger.LogInformation($"Obteniendo nomina con ID: {id}");

                var payroll = await _payrollRepo.GetByIdAsync(id);

                if (payroll == null)
                {
                    _logger.LogWarning($"No se encontró ningúna nomina con ID: {id}");
                    return NotFound("Nomina no encontrada.");
                }

                return Ok(_mapper.Map<PayrollDto>(payroll));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener la nomina con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener la nomina.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PayrollDto>> PostPayroll(PayrollCreateDto createDto)
        {
            if (createDto == null)
            {
                _logger.LogError("Se recibió una nomina nulo en la solicitud.");
                return BadRequest("La nomina no puede ser nulo.");
            }

            try
            {
                _logger.LogInformation($"Creando una nueva nomina en la fecha: {createDto.FechaPeriodo}");


                var payrollExists = await _payrollRepo
                    .GetAsync(s => s.FechaPeriodo != null && s.FechaPeriodo
                    == createDto.FechaPeriodo);

                if (payrollExists != null)
                {
                    _logger.LogWarning($"La nomina con fecha: '{createDto.FechaPeriodo}' ya existe.");
                    ModelState.AddModelError("NominaExiste", "¡La Nomina con esa fecha ya existe!");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("El modelo de Nomina no es válido.");
                    return BadRequest(ModelState);
                }


                var newPayroll = _mapper.Map<Payroll>(createDto);

                await _payrollRepo.CreateAsync(newPayroll);

                _logger.LogInformation($"Nuevo Nominca con fecha '{createDto.FechaPeriodo}' creado con ID: " +
                    $"{newPayroll.PayrollId}");
                return CreatedAtAction(nameof(GetPayroll), new { id = newPayroll.PayrollId }, newPayroll);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear una nueva nomina: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al crear una nueva nomina.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPayroll(int id, PayrollUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.PayrollId)
            {
                return BadRequest("Los datos de entrada no son válidos " +
                    "o el ID de la nomina no coincide.");
            }

            try
            {
                _logger.LogInformation($"Actualizando nomina con ID: {id}");

                var existingPayroll = await _payrollRepo.GetByIdAsync(id);
                if (existingPayroll == null)
                {
                    _logger.LogWarning($"No se encontró ningúna nomina con ID: {id}");
                    return NotFound("La Nomina no existe.");
                }


                _mapper.Map(updateDto, existingPayroll);

                await _payrollRepo.SaveChangesAsync();

                _logger.LogInformation($"Nomina con ID {id} actualizado correctamente.");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await _payrollRepo.ExistsAsync(s => s.PayrollId == id))
                {
                    _logger.LogWarning($"No se encontró ningúna Nomina con ID: {id}");
                    return NotFound("La Nomina no se encontró durante la actualización.");
                }
                else
                {
                    _logger.LogError($"Error de concurrencia al actualizar la Nomina " +
                        $"con ID: {id}. Detalles: {ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error interno del servidor al actualizar la nomina.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar la Nomina con ID {id}: " +
                    $"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al actualizar la Nomina.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePayroll(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando nomina con ID: {id}");

                var payroll = await _payrollRepo.GetByIdAsync(id);
                if (payroll == null)
                {
                    _logger.LogWarning($"No se encontró ningúna nomina con ID: {id}");
                    return NotFound("Nomina no encontrada.");
                }

                await _payrollRepo.DeleteAsync(payroll);

                _logger.LogInformation($"Nomina con ID {id} eliminada correctamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar la nomina con ID {id}: " +
                    $"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Se produjo un error al eliminar la nomina.");
            }
        }
    }
}
