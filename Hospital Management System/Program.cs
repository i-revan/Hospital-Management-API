
using Hospital_Management_System.DAL;
using Hospital_Management_System.Repositories.Implementations;
using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.Service.Implementations;
using Hospital_Management_System.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentServices,DepartmentServices>();
            builder.Services.AddScoped<IDoctorRepository,DoctorRepository>();
            builder.Services.AddScoped<IDoctorServices,DoctorServices>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
                ));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
