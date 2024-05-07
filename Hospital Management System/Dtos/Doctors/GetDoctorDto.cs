namespace Hospital_Management_System.Dtos.Doctors
{
    public class GetDoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public bool IsAvailable { get; set; }
        public string department { get; set; }
    }
}
