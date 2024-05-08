namespace Hospital_Management_System.Dtos.Appointments
{
    public class GetAppointmentDto
    {
        public int Id { get; set; }
        public string Doctor { get; set; }
        public string Patient { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
