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

        public PatientServices(IPatientRepository repository)
        {
            _repository = repository;
        }


        public async Task<ICollection<GetAllPatientsDto>> GetAllAsync()
        {
            ICollection<Patient> patients = await _repository.GetAll().ToListAsync();
            ICollection<GetAllPatientsDto> dtos = new List<GetAllPatientsDto>();
            foreach (Patient patient in patients)
            {
                dtos.Add(new GetAllPatientsDto
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Surname = patient.Surname
                });
            }
            return dtos;
        }

        public async Task<GetPatientDto> GetByIdAsync(int id)
        {
            Patient patient = await _repository.GetByIdAsync(id, p=>p.Appointments);
            if (patient is null) throw new Exception("Not found");
            return new GetPatientDto
            {
                Id = id,
                Name = patient.Name,
                Surname = patient.Surname,
                Address = patient.Address,
                Age = patient.Age,
                Appointments = patient.Appointments
            };
        }

        public async Task CreateAsync(CreatePatientDto dto)
        {
            await _repository.AddAsync(new Patient { Name = dto.Name, Surname = dto.Surname, Address = dto.Address, Age = dto.Age });
            await _repository.SaveChangeAsync();
        }


        public async Task<UpdatePatientDto> UpdateAsync(int id, UpdatePatientDto dto)
        {
            Patient patient = await _repository.GetByIdAsync(id);
            if (patient is null) throw new Exception("Not found");
            patient.Name = dto.Name;
            patient.Surname = dto.Surname;
            patient.Address = dto.Address;
            patient.Age = dto.Age;
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
