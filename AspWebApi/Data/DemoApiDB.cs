using AspWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AspWebApi.Data
{
    public class DemoApiDB : DbContext
    {
        public DemoApiDB(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ClassDemo> ClassDemos { get; set; }
    }
}
