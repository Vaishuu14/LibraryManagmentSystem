using LibraryManagmentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Infrastructure.Data
{
    public class LibraryDBContext : DbContext
    {
        public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Fine> Fines { get; set; }

        public DbSet<User> Users { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Loans)
                .WithOne(l => l.Book)
                .HasForeignKey(l => l.BookId);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Reservations)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId);

            modelBuilder.Entity<Member>()
               .HasMany(m => m.Loans)
               .WithOne(l => l.Member)
               .HasForeignKey(l => l.MemberId);

            modelBuilder.Entity<Member>()
               .HasMany(m => m.Reservations)
               .WithOne(r => r.Member)
               .HasForeignKey(r => r.MemberId);

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId);

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Member)
                .WithMany(m => m.Loans)
                .HasForeignKey(l => l.MemberId);

            modelBuilder.Entity<Fine>()
                .HasOne(f => f.Loan)
                .WithMany(l => l.Fines)
                .HasForeignKey(f => f.LoanId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reservations)
                .HasForeignKey(r => r.BookId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Member)
                .WithMany(m => m.Reservations)
                .HasForeignKey(r => r.MemberId);

            //modelBuilder.Entity<Fine>()
            //      .Property(f => f.Amount)
            //      .HasPrecision(18, 2);



            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "9780743273565",PublishedDate=new DateTime(2020,1,1), NumberOfCopies = 10 },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", ISBN = "9780060935467", PublishedDate = new DateTime(2020, 4, 5), NumberOfCopies = 8 },
                new Book { Id = 3, Title = "1984", Author = "George Orwell", ISBN = "9780451524935", PublishedDate = new DateTime(2020, 1, 1), NumberOfCopies = 12 },
                new Book { Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen", ISBN = "9780141040349", PublishedDate = new DateTime(2020, 5, 7), NumberOfCopies = 7 },
                new Book { Id = 5, Title = "The Catcher in the Rye", Author = "J.D. Salinger", ISBN = "9780316769488", PublishedDate = new DateTime(2020, 10, 10), NumberOfCopies = 6 }
               
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "admin", Password = "admin", Email = "vaishu@gmail.com", Role = "Admin" });
        }


    }
}
