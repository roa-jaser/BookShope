 
using BookShope.Models;
using Microsoft.EntityFrameworkCore; //use this package because DbContext is installed with it

namespace BookShope.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        { }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<favoriteBook> Favs { get; set; } = null!;
        public DbSet<Admin> Admin { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Book_Id = 1,
                    Book_Name = "The book of the people",
                    Writer = "Tsahor, Dan",
                    price = 10,
                }
            );
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Book_Id = 2,
                    Book_Name = "The Atomic Habits",
                    Writer = "James, Clear",
                    price = 9,
                }
            );
            modelBuilder.Entity<Admin>().HasData(
                 new Admin
                 {
                     Admin_Id = 1,
                     User_Name = "hiba",
                     Email = "hiba@gmail.com",
                     Password = "123",
                     Flag = 1
                 }
                );
        }



    }

}












