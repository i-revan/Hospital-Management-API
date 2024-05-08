using Hospital_Management_System.Repositories.Interfaces;

namespace Hospital_Management_System.Repositories.Implementations
{
    public class AppointmentRepository:Repository<Appointment>,IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
