using Hospital_Management_System.Dtos.Doctors;
using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _repository;
        private readonly IDoctorServices _service;

        public DoctorsController(IDoctorRepository repository, IDoctorServices service)
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
            GetDoctorDto doctorDetailsDto = await _service.GetByIdAsync(id);
            return Ok(doctorDetailsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateDoctorDto doctorDto)
        {
            await _service.CreateAsync(doctorDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] UpdateDoctorDto doctorDto)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.UpdateAsync(id, doctorDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Doctor existed = await _repository.GetByIdAsync(id);
            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);
            _repository.Delete(existed);
            await _repository.SaveChangeAsync();
            return NoContent();
        }
    }
}
