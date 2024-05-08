using Hospital_Management_System.Dtos.Doctors;
using Hospital_Management_System.Dtos.Patients;
using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _repository;
        private readonly IPatientServices _service;

        public PatientsController(IPatientRepository repository, IPatientServices service)
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
            Patient patient = await _repository.GetByIdAsync(id, p => p.Appointments);
            GetPatientDto patientDto = new GetPatientDto
            {
                Id = patient.Id,
                Name = patient.Name,
                Surname = patient.Surname,
                Address = patient.Address
            };
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreatePatientDto patientDto)
        {
            await _service.CreateAsync(patientDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromForm] UpdatePatientDto patientDto)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.UpdateAsync(id, patientDto);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Patient existed = await _repository.GetByIdAsync(id);
            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);
            _repository.Delete(existed);
            await _repository.SaveChangeAsync();
            return NoContent();
        }
    }
}
