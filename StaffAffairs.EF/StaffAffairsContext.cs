using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffAffairs.Core.Models;




namespace  EF
{
    public  class StaffAffairsContext : DbContext
    {
        public StaffAffairsContext(DbContextOptions<StaffAffairsContext> options) : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<MilitaryState> MilitaryStates { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobType> jobTypes { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Social> Socials { get; set; }

        public DbSet<University> Universities { get; set; }
        public DbSet<WorkStatus> workStatuses { get; set; }
        public DbSet<EntedabType> EntedabTypes { get; set; }
    }
}
