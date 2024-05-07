namespace Hospital_Management_System.Dtos.Doctors
{
    public class UpdateDoctorDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public bool IsAvailable { get; set; }
        public int DepartmentId { get; set; }
    }
}
