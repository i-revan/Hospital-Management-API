namespace Hospital_Management_System.Dtos.Doctors
{
    public class CreateDoctorDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
    }
}
