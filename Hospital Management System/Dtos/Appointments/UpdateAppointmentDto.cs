namespace Hospital_Management_System.Dtos.Appointments
{
    public class UpdateAppointmentDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
