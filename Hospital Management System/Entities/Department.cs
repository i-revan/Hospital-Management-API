using Hospital_Management_System.Entities.Base;

namespace Hospital_Management_System.Entities
{
    public class Department:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
