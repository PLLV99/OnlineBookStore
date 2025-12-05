using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace OnlineBookStore.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Seed Data
            builder.Entity<Book>().HasData(
                new Book { BookId = 1, ISBN = "978-1", Name = "C# Mastery", Genre = "Tech", PublishedYear = 2024, Author = "John Doe", Price = 50 },
                new Book { BookId = 2, ISBN = "978-2", Name = "Harry Potter", Genre = "Fantasy", PublishedYear = 2001, Author = "J.K. Rowling", Price = 40 },
                new Book { BookId = 3, ISBN = "978-3", Name = "Clean Code", Genre = "Tech", PublishedYear = 2008, Author = "Uncle Bob", Price = 60 }
            );
        }
    }
}