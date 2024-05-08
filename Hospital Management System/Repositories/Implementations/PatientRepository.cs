using Hospital_Management_System.Repositories.Interfaces;

namespace Hospital_Management_System.Repositories.Implementations
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
