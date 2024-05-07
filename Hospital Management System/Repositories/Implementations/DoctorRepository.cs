using Hospital_Management_System.Repositories.Interfaces;

namespace Hospital_Management_System.Repositories.Implementations
{
    public class DoctorRepository:Repository<Doctor>,IDoctorRepository
    {
        public DoctorRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
