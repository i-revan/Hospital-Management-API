namespace Hospital_Management_System.Dtos.Appointments
{
    public class CreateAppointmentDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
