using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Service.Implementations
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentServices(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<GetDepartmentDto>> GetAllAsync()
        {
            ICollection<Department> departments = await _repository.GetAll().ToListAsync();
            ICollection<GetDepartmentDto> dtos = new List<GetDepartmentDto>();
            foreach (Department department in departments)
            {
                dtos.Add(new GetDepartmentDto
                {
                    Id = department.Id,
                    Name = department.Name
                });
            }
            return dtos;
        }

        public async Task<GetDepartmentDto> GetByIdAsync(int id)
        {
            Department department = await _repository.GetByIdAsync(id);
            if (department is null) throw new Exception("Not found");
            return new GetDepartmentDto { Id = id, Name = department.Name };
        }

        public async Task CreateAsync(CreateDepartmentDto dto)
        {
            await _repository.AddAsync(new Department { Name = dto.Name });
            await _repository.SaveChangeAsync();
        }

        public async Task<UpdateDepartmentDto> UpdateAsync(int id,UpdateDepartmentDto dto)
        {
            Department department = await _repository.GetByIdAsync(id);
            if (department is null) throw new Exception("Not found");
            department.Name = dto.Name;
            _repository.Update(department);
            await _repository.SaveChangeAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            Department department = await _repository.GetByIdAsync(id);
            if (department is null) throw new Exception("Not found");
            _repository.Delete(department);
            await _repository.SaveChangeAsync();
        }
    }
}
