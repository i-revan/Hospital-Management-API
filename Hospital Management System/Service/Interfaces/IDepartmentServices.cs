
namespace Hospital_Management_System.Service.Interfaces
{
    public interface IDepartmentServices
    {
        Task<ICollection<GetAllDepartmentsDto>> GetAllAsync();
        Task<GetDepartmentDto> GetByIdAsync(int id);
        Task CreateAsync(CreateDepartmentDto dto);
        Task<UpdateDepartmentDto> UpdateAsync(int id,UpdateDepartmentDto dto);
        Task DeleteAsync(int id);
    }
}
