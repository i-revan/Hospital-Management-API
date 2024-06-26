﻿using Hospital_Management_System.Entities.Base;
using System.Text.Json.Serialization;

namespace Hospital_Management_System.Entities
{
    public class Doctor:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public bool IsAvailable { get; set; }
        public int DepartmentId { get; set; }

        [JsonIgnore]
        public Department Department { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
