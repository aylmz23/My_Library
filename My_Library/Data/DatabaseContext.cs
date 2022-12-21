using Microsoft.EntityFrameworkCore;
using My_Library.Entities;
using My_Library.Models;

namespace My_Library.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<BookType> BookTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; database=My_Library; integrated security=true;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
