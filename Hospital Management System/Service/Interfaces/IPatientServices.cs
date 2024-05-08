using Hospital_Management_System.Dtos.Patients;

namespace Hospital_Management_System.Service.Interfaces
{
    public interface IPatientServices
    {
        Task<ICollection<GetAllPatientsDto>> GetAllAsync();
        Task<GetPatientDto> GetByIdAsync(int id);
        Task CreateAsync(CreatePatientDto dto);
        Task<UpdatePatientDto> UpdateAsync(int id, UpdatePatientDto dto);
        Task DeleteAsync(int id);
    }
}
