using Hospital_Management_System.Entities.Base;

namespace Hospital_Management_System.Entities
{
    public class Patient:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
