using Hospital_Management_System.DAL;
using Hospital_Management_System.Entities;
using Hospital_Management_System.Repositories.Interfaces;

namespace Hospital_Management_System.Repositories.Implementations
{
    public class DepartmentRepository:Repository<Department>,IDepartmentRepository
    {

        public DepartmentRepository(AppDbContext context):base(context)
        {
            
        }

    }
}
