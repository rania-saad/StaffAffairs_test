
using Microsoft.EntityFrameworkCore;
using StaffAffairs.Core.Interfaces;
using StaffAffairs.Core.Models;
using StaffAffairs.Core.Services;
using StaffAffairs.EF;
using StaffAffairs.Infrastructure.Data;
namespace StaffAffairs.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StaffAffairsContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Add this line to your ConfigureServices method (or after builder.Services in Program.cs)


            builder.Services.AddScoped<IRepository<Nationality>, Repository<Nationality>>();

            builder.Services.AddScoped<INationalityService, NationalityService>();

            builder.Services.AddScoped<IRepository<Social>, Repository<Social>>();

            builder.Services.AddScoped<ISocialService, SocialService>();

            builder.Services.AddScoped<IRepository<WorkStatus>, Repository<WorkStatus>>();

            builder.Services.AddScoped<IWorkStatusService, WorkStatusService>();

            builder.Services.AddScoped<IRepository<MilitaryState>, Repository<MilitaryState>>();

            builder.Services.AddScoped<IMilitaryStateService, MilitaryStateService>();

            builder.Services.AddScoped<IRepository<University>, Repository<University>>();

            builder.Services.AddScoped<IUniversityService, UniversityService>();

            builder.Services.AddScoped<IRepository<Faculty>, Repository<Faculty>>();

            builder.Services.AddScoped<IFacultyService, FacultyService>();

            builder.Services.AddScoped<IRepository<Department>, Repository<Department>>();

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            builder.Services.AddScoped<IRepository<JobType>, Repository<JobType>>();

            builder.Services.AddScoped<IJobTypeService, JobTypeService>();

            builder.Services.AddScoped<IRepository<Job>, Repository<Job>>();

            builder.Services.AddScoped<IJobService, JobService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
