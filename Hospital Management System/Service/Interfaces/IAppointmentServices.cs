using Hospital_Management_System.Dtos.Appointments;
using Hospital_Management_System.Dtos.Patients;

namespace Hospital_Management_System.Service.Interfaces
{
    public interface IAppointmentServices
    {
        Task<ICollection<GetAllAppointmentsDto>> GetAllAsync();
        Task<GetAppointmentDto> GetByIdAsync(int id);
        Task CreateAsync(CreateAppointmentDto dto);
        Task<UpdateAppointmentDto> UpdateAsync(int id, UpdateAppointmentDto dto);
        Task DeleteAsync(int id);
    }
}
