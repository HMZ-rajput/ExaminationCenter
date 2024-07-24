using ExaminationCenter.Models;
using Microsoft.EntityFrameworkCore;

namespace ExaminationCenter.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Request> requests { get; set; }
        public DbSet<Examination> examinations { get; set; }
    }
}
