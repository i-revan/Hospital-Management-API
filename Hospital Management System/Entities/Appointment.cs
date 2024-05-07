using Hospital_Management_System.Entities.Base;

namespace Hospital_Management_System.Entities
{
    public class Appointment:BaseEntity
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
