namespace Hospital_Management_System.Dtos.Patients
{
    public class GetPatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
