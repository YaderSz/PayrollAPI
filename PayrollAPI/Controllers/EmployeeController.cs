using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrolAPI.Repository.IRepository;
using SharedModels;
using SharedModels.Dto;

namespace PayrolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
  
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IPayrollRepository _payrollRepo;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        public EmployeeController ( IEmployeeRepository employeeRepo,IPayrollRepository payrollRepo ,ILogger<EmployeeController> logger, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _payrollRepo = payrollRepo;
            _logger = logger;
                
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EmployesDto>>> GetEmployees()
        {
            try
            {
                _logger.LogInformation("Obteniendo a los empleados");
                var employee = await _employeeRepo.GetAllAsync();

                return Ok(_mapper.Map<IEnumerable<EmployesDto>>(employee));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener a los empleados: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener a los empleados");

            }
        }
     
        [HttpGet("{id}")]
       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployesDto>> GetEmployee(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"ID del empleado no válido: {id}");
                return BadRequest("ID del empleado no válido");
            }

            try
            {
                _logger.LogInformation($"Obteniendo empleado con ID: {id}");

                var employee = await _employeeRepo.GetByIdAsync(id);

                if (employee == null)
                {
                    _logger.LogWarning($"No se encontró ningún empleado con ID: {id}");
                    return NotFound("Empleado no encontrado.");
                }

                return Ok(_mapper.Map<EmployesDto>(employee));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener al empleado con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al obtener al empleado.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployesDto>> PostEmployee(EmployeeCreateDto createDto)
        {
            if (createDto == null)
            {
                _logger.LogError("Se recibió un  empleado nulo en la solicitud.");
                return BadRequest("El empleado no puede ser nulo.");
            }

            try
            {
                _logger.LogInformation($"Creando un nuevo Empleado para la nomina con ID: {createDto.PayrollId}");
                var payrollExists = await _payrollRepo
                    .ExistsAsync(s => s.PayrollId == createDto.PayrollId);

                if (!payrollExists)
                {
                    _logger.LogWarning($"La nomina con ID '{createDto.PayrollId}' no existe.");
                    ModelState.AddModelError("EmpleadoNoExiste", "¡El empleado no existe!");
                    return BadRequest(ModelState);
                }

                var existingEmpleado = await _employeeRepo
                    .GetAsync(s => s.Cedula != null && s.Cedula.ToLower()
                    == createDto.Cedula.ToLower());

                if (existingEmpleado != null)
                {
                    _logger.LogWarning($"El empleado para la nomina con ID '{createDto.PayrollId}' ya existe.");
                    ModelState.AddModelError("NombreExiste", "¡El empleado para esa nomina ya existe!");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("El modelo de estudiante no es válido.");
                    return BadRequest(ModelState);
                }

   
                var newEmployee = _mapper.Map<Employee>(createDto);

                await _employeeRepo.CreateAsync(newEmployee);

                _logger.LogInformation($"Nuevo empleado con cedula '{createDto.Cedula}' creado con ID: " +
                    $"{newEmployee.EmployeeID}");
                return CreatedAtAction(nameof(GetEmployee), new { id = newEmployee.EmployeeID }, newEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear un nuevo empleado: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al crear un nuevo empleado.");
            }
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("CalculateTotalEmployee/{employeeId}")]
        public async Task<IActionResult> CalculateNetSalary(int employeeId)
        {

                var netSalary = await _employeeRepo.CalculateNetSalaryAsync(employeeId);
                return Ok(netSalary);
            
     
    
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutEmployee(int id, EmployeeUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.EmployeeID)
            {
                return BadRequest("Los datos de entrada no son válidos " +
                    "o el ID del empleado no coincide.");
            }

            try
            {
                _logger.LogInformation($"Actualizando empleado con ID: {id}");

                var existingEmployee = await _employeeRepo.GetByIdAsync(id);
                if (existingEmployee == null)
                {
                    _logger.LogWarning($"No se encontró ningún empleado con ID: {id}");
                    return NotFound("El empleado no existe.");
                }
                var payrollExists = await _payrollRepo
                   .ExistsAsync(s => s.PayrollId == updateDto.PayrollId);

                if (!payrollExists)
                {
                    _logger.LogWarning($"La nomina con ID '{updateDto.PayrollId}' no existe.");
                    ModelState.AddModelError("NominaNoExiste", "¡La nomina no existe!");
                    return BadRequest(ModelState);
                }

                _mapper.Map(updateDto, existingEmployee);

                await _employeeRepo.SaveChangesAsync();

                _logger.LogInformation($"Empleado con ID {id} actualizado correctamente.");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await _employeeRepo.ExistsAsync(s => s.EmployeeID == id))
                {
                    _logger.LogWarning($"No se encontró ningún empleado con ID: {id}");
                    return NotFound("El empleado no se encontró durante la actualización.");
                }
                else
                {
                    _logger.LogError($"Error de concurrencia al actualizar el empleado " +
                        $"con ID: {id}. Detalles: {ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error interno del servidor al actualizar al empleado.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar al empleado con ID {id}: " +
                    $"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error interno del servidor al actualizar al empleado.");
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando empleado con ID: {id}");

                var employee = await _employeeRepo.GetByIdAsync(id);
                if (employee == null)
                {
                    _logger.LogWarning($"No se encontró ningún empleado con ID: {id}");
                    return NotFound("Empleado no encontrado.");
                }

                await _employeeRepo.DeleteAsync(employee);

                _logger.LogInformation($"Empleado con ID {id} eliminado correctamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar al empleado con ID {id}: " +
                    $"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Se produjo un error al eliminar al empleado.");
            }
        }
    }
}
