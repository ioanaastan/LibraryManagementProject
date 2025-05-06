using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<BorrowingRecord> BorrowingRecords { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Book entity
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Author).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ISBN).HasMaxLength(20);
            entity.Property(e => e.Publisher).HasMaxLength(100);
            entity.Property(e => e.Genre).HasMaxLength(50);
        });

        // Configure BorrowingRecord entity
        modelBuilder.Entity<BorrowingRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.BorrowerName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.BorrowerEmail).IsRequired().HasMaxLength(100);

            // Configure one-to-many relationship
            entity.HasOne(e => e.Book)
                  .WithMany(b => b.BorrowingRecords)
                  .HasForeignKey(e => e.BookId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}