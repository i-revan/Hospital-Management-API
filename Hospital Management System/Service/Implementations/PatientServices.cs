using AutoMapper;
using Hospital_Management_System.Dtos.Patients;
using Hospital_Management_System.Entities;
using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Service.Implementations
{
    public class PatientServices : IPatientServices
    {
        private readonly IPatientRepository _repository;
        private readonly IMapper _mapper;

        public PatientServices(IPatientRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetAllPatientsDto>> GetAllAsync()
        {
            ICollection<Patient> patients = await _repository.GetAll().ToListAsync();
            ICollection<GetAllPatientsDto> dtos = new List<GetAllPatientsDto>();
            foreach (Patient patient in patients)
            {
                GetAllPatientsDto dto = _mapper.Map<GetAllPatientsDto>(patient);
                dtos.Add(dto);
            }
            return dtos;
        }

        public async Task<GetPatientDto> GetByIdAsync(int id)
        {
            Patient patient = await _repository.GetByIdAsync(id, p=>p.Appointments);
            if (patient is null) throw new Exception("Not found");
            GetPatientDto dto = _mapper.Map<GetPatientDto>(patient);
            return dto;
        }

        public async Task CreateAsync(CreatePatientDto dto)
        {
            Patient patient = _mapper.Map<Patient>(dto);
            await _repository.AddAsync(patient);
            await _repository.SaveChangeAsync();
        }


        public async Task<UpdatePatientDto> UpdateAsync(int id, UpdatePatientDto dto)
        {
            Patient patient = await _repository.GetByIdAsync(id);
            if (patient is null) throw new Exception("Not found");
            _mapper.Map(dto, patient);
            _repository.Update(patient);
            await _repository.SaveChangeAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            Patient patient = await _repository.GetByIdAsync(id);
            if (patient is null) throw new Exception("Not found");
            _repository.Delete(patient);
            await _repository.SaveChangeAsync();
        }
    }

}
