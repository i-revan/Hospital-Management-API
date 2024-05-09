using AutoMapper;
using Hospital_Management_System.Dtos.Doctors;
using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Service.Implementations
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;

        public DoctorServices(IDoctorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetAllDoctorsDto>> GetAllAsync()
        {
            ICollection<Doctor> doctors = await _repository.GetAll().Include(d=>d.Department).ToListAsync();
            ICollection<GetAllDoctorsDto> dtos = new List<GetAllDoctorsDto>();
            foreach (Doctor doctor in doctors)
            {
                GetAllDoctorsDto dto = _mapper.Map<GetAllDoctorsDto>(doctor);
                dto.department = doctor.Department.Name;
                dtos.Add(dto);
            }
            return dtos;
        }

        public async Task<GetDoctorDto> GetByIdAsync(int id)
        {
            Doctor doctor = await _repository.GetByIdAsync(id, d => d.Department,d=>d.Appointments);
            if (doctor is null) throw new Exception("Not found");
            GetDoctorDto dto = _mapper.Map<GetDoctorDto>(doctor);
            dto.department = doctor.Department.Name;
            return dto;
        }

        public async Task CreateAsync(CreateDoctorDto dto)
        {
            Doctor doctor = _mapper.Map<Doctor>(dto);
            await _repository.AddAsync(doctor);
            await _repository.SaveChangeAsync();
        }

        public async Task<UpdateDoctorDto> UpdateAsync(int id, UpdateDoctorDto dto)
        {
            Doctor doctor = await _repository.GetByIdAsync(id);
            if (doctor is null) throw new Exception("Not found");
            _mapper.Map(dto, doctor);
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
