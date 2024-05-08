using Hospital_Management_System.Entities.Base;
using System.Text.Json.Serialization;

namespace Hospital_Management_System.Entities
{
    public class Appointment:BaseEntity
    {
        public int PatientId { get; set; }
        [JsonIgnore]
        public Patient Patient { get; set; }

        public int DoctorId { get; set; }
        [JsonIgnore]
        public Doctor Doctor { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
