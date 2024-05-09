using AutoMapper;
using Hospital_Management_System.Dtos.Appointments;
using Hospital_Management_System.Dtos.Doctors;
using Hospital_Management_System.Dtos.Patients;

namespace Hospital_Management_System.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            //Departments
            CreateMap<Department, CreateDepartmentDto>().ReverseMap();
            CreateMap<Department,GetAllDepartmentsDto>().ReverseMap();
            CreateMap<Department,GetDepartmentDto>().ReverseMap();
            CreateMap<Department,UpdateDepartmentDto>().ReverseMap();

            //Doctors
            CreateMap<Doctor,GetAllDoctorsDto>().ReverseMap();
            CreateMap<Doctor, GetDoctorDto>().ReverseMap();
            CreateMap<Doctor,CreateDoctorDto>().ReverseMap();
            CreateMap<Doctor, UpdateDoctorDto>().ReverseMap();

            //Patients
            CreateMap<Patient, GetAllPatientsDto>().ReverseMap();
            CreateMap<Patient, GetPatientDto>().ReverseMap();
            CreateMap<Patient, UpdatePatientDto>().ReverseMap();
            CreateMap<Patient, CreatePatientDto>().ReverseMap();

            //Appointments
            CreateMap<Appointment, GetAllAppointmentsDto>().ReverseMap();
            CreateMap<Appointment, GetAppointmentDto>().ReverseMap();
            CreateMap<Appointment, CreateAppointmentDto>().ReverseMap();
            CreateMap<Appointment, UpdateAppointmentDto>().ReverseMap();
        }
    }
}
