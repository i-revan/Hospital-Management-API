using AutoMapper;
using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Service.Implementations
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public DepartmentServices(IDepartmentRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetAllDepartmentsDto>> GetAllAsync()
        {
            ICollection<Department> departments = await _repository.GetAll().Include(d => d.Doctors).ToListAsync();
            ICollection<GetAllDepartmentsDto> dtos = new List<GetAllDepartmentsDto>();
            foreach (Department department in departments)
            {
                GetAllDepartmentsDto dto = _mapper.Map<GetAllDepartmentsDto>(department);
                dto.DoctorsNumber = department.Doctors.Count;
                dtos.Add(dto);
            }
            return dtos;
        }

        public async Task<GetDepartmentDto> GetByIdAsync(int id)
        {
            Department department = await _repository.GetByIdAsync(id, d=>d.Doctors);
            if (department is null) throw new Exception("Not found");
            GetDepartmentDto dto = _mapper.Map<GetDepartmentDto>(department);
            return dto;
        }

        public async Task CreateAsync(CreateDepartmentDto dto)
        {
            Department department = _mapper.Map<Department>(dto);
            await _repository.AddAsync(department);
            await _repository.SaveChangeAsync();
        }

        public async Task<UpdateDepartmentDto> UpdateAsync(int id,UpdateDepartmentDto dto)
        {
            Department department = await _repository.GetByIdAsync(id);
            if (department is null) throw new Exception("Not found");
            _mapper.Map(dto, department);
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
