using Hospital_Management_System.Dtos.Doctors;

namespace Hospital_Management_System.Service.Interfaces
{
    public interface IDoctorServices
    {
        Task<ICollection<GetAllDoctorsDto>> GetAllAsync();
        Task<GetDoctorDto> GetByIdAsync(int id);
        Task CreateAsync(CreateDoctorDto dto);
        Task<UpdateDoctorDto> UpdateAsync(int id, UpdateDoctorDto dto);
        Task DeleteAsync(int id);
    }
}
