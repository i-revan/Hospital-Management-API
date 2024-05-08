using Hospital_Management_System.Dtos.Appointments;
using Hospital_Management_System.Dtos.Doctors;
using Hospital_Management_System.Repositories.Implementations;
using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Service.Implementations
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly IAppointmentRepository _repository;
        private readonly IDoctorRepository _doctorRepository;

        public AppointmentServices(IAppointmentRepository repository, IDoctorRepository doctorRepository)
        {
            _repository = repository;
            _doctorRepository = doctorRepository;
        }
        public async Task<ICollection<GetAllAppointmentsDto>> GetAllAsync()
        {
            ICollection<Appointment> appointments = await _repository.GetAll().Include(a => a.Doctor)
                .Include(a => a.Patient).ToListAsync();
            ICollection<GetAllAppointmentsDto> dtos = new List<GetAllAppointmentsDto>();
            foreach (Appointment appointment in appointments)
            {
                dtos.Add(new GetAllAppointmentsDto
                {
                    Patient = appointment.Patient.Name +" " + appointment.Patient.Surname,
                    Doctor = appointment.Doctor.Name + " " + appointment.Doctor.Surname
                });
            }
            return dtos;
        }

        public async Task<GetAppointmentDto> GetByIdAsync(int id)
        {
            Appointment appointment = await _repository.GetByIdAsync(id, a => a.Doctor, a => a.Patient);
            if (appointment is null) throw new Exception("Not found");
            return new GetAppointmentDto
            {
                Id = id,
                Patient = appointment.Patient.Name + appointment.Patient.Surname,
                Doctor = appointment.Doctor.Name + appointment.Patient.Surname,
                Start = appointment.Start,
                End = appointment.End
            };
        }

        public async Task CreateAsync(CreateAppointmentDto dto)
        {
            if (dto.Start.TimeOfDay < TimeSpan.FromHours(8) || dto.End.TimeOfDay > TimeSpan.FromHours(20))
            {
                throw new Exception("Appointment must be scheduled between 08:00 and 20:00.");
            }
            Doctor doctor = await _doctorRepository.GetByIdAsync(dto.DoctorId);

            // Check if the doctor is available
            if (!doctor.IsAvailable)
            {
                throw new Exception("Doctor is not available!");
            }

            // Check for overlapping appointments
            foreach (Appointment appointment in doctor.Appointments)
            {
                if (dto.Start < appointment.End && dto.End > appointment.Start)
                {
                    throw new Exception("Appointment time conflicts with existing appointment.");
                }
            }

            // Create the appointment
            await _repository.AddAsync(new Appointment { DoctorId = dto.DoctorId, PatientId = dto.PatientId, Start = dto.Start, End = dto.End });
            await _repository.SaveChangeAsync();
        }

        public async Task<UpdateAppointmentDto> UpdateAsync(int id, UpdateAppointmentDto dto)
        {
            Appointment appointment = await _repository.GetByIdAsync(id);
            if (appointment is null) throw new Exception("Not found");
            appointment.DoctorId = dto.DoctorId;
            appointment.PatientId = dto.PatientId;
            appointment.Start = dto.Start;
            appointment.End = dto.End;
            _repository.Update(appointment);
            await _repository.SaveChangeAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            Appointment appointment = await _repository.GetByIdAsync(id);
            if (appointment is null) throw new Exception("Not found");
            _repository.Delete(appointment);
            await _repository.SaveChangeAsync();
        }
    }
}
