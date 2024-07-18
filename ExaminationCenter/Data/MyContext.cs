using ExaminationCenter.Models;
using Microsoft.EntityFrameworkCore;

namespace ExaminationCenter.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        //for migration uncomment this
        //public MyContext() { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=DELL-M3800\\MSSQLSERVER01; Initial Catalog=ExaminationDb; User Id=local; Password=123; encrypt=False; MultipleActiveResultSets=True; App=EntityFramework;");
        //}

        public DbSet<User> users { get; set; }
        public DbSet<Request> requests { get; set; }
        public DbSet<Examination> examinations { get; set; }
    }
}
