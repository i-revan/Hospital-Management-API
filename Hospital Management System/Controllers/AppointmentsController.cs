using Hospital_Management_System.Dtos.Appointments;
using Hospital_Management_System.Dtos.Doctors;
using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository _repository;
        private readonly IAppointmentServices _service;

        public AppointmentsController(IAppointmentRepository repository,IAppointmentServices service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAllAsync());
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Appointment appointment = await _repository.GetByIdAsync(id, a=> a.Patient, a=>a.Doctor);
            GetAppointmentDto appointmentDto = new GetAppointmentDto
            {
                Id = appointment.Id,
                Patient = appointment.Patient.Name + appointment.Patient.Surname,
                Doctor = appointment.Doctor.Name + appointment.Doctor.Surname
            };
            return Ok(appointmentDto);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateAppointmentDto appointmentDto)
        {
            await _service.CreateAsync(appointmentDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] UpdateAppointmentDto dto)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Appointment existed = await _repository.GetByIdAsync(id);
            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);
            _repository.Delete(existed);
            await _repository.SaveChangeAsync();
            return NoContent();
        }

    }
}
