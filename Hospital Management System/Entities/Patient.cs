﻿using Hospital_Management_System.Entities.Base;
using System.Text.Json.Serialization;

namespace Hospital_Management_System.Entities
{
    public class Patient:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        [JsonIgnore]
        public ICollection<Appointment> Appointments { get; set; }
    }
}
