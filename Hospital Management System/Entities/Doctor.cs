using Hospital_Management_System.Entities.Base;

namespace Hospital_Management_System.Entities
{
    public class Doctor:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
