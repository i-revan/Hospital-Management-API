using Hospital_Management_System.Dtos.Departments;
using Hospital_Management_System.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentsController(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.GetAll());
        }
        [HttpGet("{id}")]
        //[Route("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                //return BadRequest();
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            Department department = await _repository.GetByIdAsync(id);
            
            if(department is null) 
            { 
                //return NotFound();
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]CreateDepartmentDto departmentDto)
        {
            Department department = new Department { Name = departmentDto.Name };
            await _repository.AddAsync(department);
            await _repository.SaveChangeAsync();
            return StatusCode(StatusCodes.Status201Created, department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] UpdateDepartmentDto departmentDto)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Department existed = await _repository.GetByIdAsync(id);

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);
            existed.Name = departmentDto.Name;
            _repository.Update(existed);
            await _repository.SaveChangeAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Department existed = await _repository.GetByIdAsync(id);
            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);
            _repository.Delete(existed);
            await _repository.SaveChangeAsync();
            return NoContent();
        }
    }
}
