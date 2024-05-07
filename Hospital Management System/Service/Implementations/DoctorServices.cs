using Hospital_Management_System.Dtos.Doctors;
using Hospital_Management_System.Entities;
using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Service.Implementations
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IDoctorRepository _repository;

        
        public DoctorServices(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<GetDoctorDto>> GetAllAsync()
        {
            ICollection<Doctor> doctors = await _repository.GetAll().Include(d=>d.Department).ToListAsync();
            ICollection<GetDoctorDto> dtos = new List<GetDoctorDto>();
            foreach (Doctor doctor in doctors)
            {
                dtos.Add(new GetDoctorDto
                {
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Surname = doctor.Surname,
                    Address = doctor.Address,
                    IsAvailable = doctor.IsAvailable,
                    department = doctor.Department.Name
                });
            }
            return dtos;
        }

        public async Task<GetDoctorDto> GetByIdAsync(int id)
        {
            Doctor doctor = await _repository.GetByIdAsync(id, d => d.Department);
            if (doctor is null) throw new Exception("Not found");
            return new GetDoctorDto { Id = id, Name = doctor.Name , Surname = doctor.Surname, 
                Address = doctor.Address, department = doctor.Department.Name};
        }

        public async Task CreateAsync(CreateDoctorDto dto)
        {
            await _repository.AddAsync(new Doctor { Name = dto.Name, Surname = dto.Surname, Address = dto.Address, IsAvailable = true,DepartmentId = dto.DepartmentId});
            await _repository.SaveChangeAsync();
        }



        public async Task<UpdateDoctorDto> UpdateAsync(int id, UpdateDoctorDto dto)
        {
            Doctor doctor = await _repository.GetByIdAsync(id);
            if (doctor is null) throw new Exception("Not found");
            doctor.Name = dto.Name;
            doctor.Surname = dto.Surname;
            doctor.Address = dto.Address;
            doctor.IsAvailable = dto.IsAvailable;
            doctor.DepartmentId = dto.DepartmentId;
            _repository.Update(doctor);
            await _repository.SaveChangeAsync();
            return dto;
        }
        public async Task DeleteAsync(int id)
        {
            Doctor doctor = await _repository.GetByIdAsync(id);
            if (doctor is null) throw new Exception("Not found");
            _repository.Delete(doctor);
            await _repository.SaveChangeAsync();
        }
    }
}
