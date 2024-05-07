﻿namespace Hospital_Management_System.Dtos.Departments
{
    public class GetDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
