using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Database;

public static class DataSeeder
{
    public static async Task SeedDataAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Apply any pending migrations
        await dbContext.Database.MigrateAsync();

        // Check if we already have books
        if (await dbContext.Books.AnyAsync())
        {
            return; // Database has been seeded
        }

        // Sample books
        var books = new List<Book>
        {
            new Book
            {
                Title = "Clean Code",
                Author = "Robert C. Martin",
                ISBN = "9780132350884",
                PublicationYear = 2008,
                Publisher = "Prentice Hall",
                Genre = "Programming",
                TotalCopies = 5,
                AvailableCopies = 3
            },
            new Book
            {
                Title = "Design Patterns",
                Author = "Erich Gamma et al.",
                ISBN = "9780201633610",
                PublicationYear = 1994,
                Publisher = "Addison-Wesley",
                Genre = "Programming",
                TotalCopies = 3,
                AvailableCopies = 1
            },
            new Book
            {
                Title = "The Pragmatic Programmer",
                Author = "Andrew Hunt, David Thomas",
                ISBN = "9780201616224",
                PublicationYear = 1999,
                Publisher = "Addison-Wesley",
                Genre = "Programming",
                TotalCopies = 4,
                AvailableCopies = 2
            }
        };

        await dbContext.Books.AddRangeAsync(books);
        await dbContext.SaveChangesAsync();

        // Sample borrowing records
        var borrowings = new List<BorrowingRecord>
        {
            // Borrowings for Clean Code
            new BorrowingRecord
            {
                BookId = books[0].Id,
                BorrowerName = "John Doe",
                BorrowerEmail = "john.doe@example.com",
                BorrowDate = DateTime.Now.AddDays(-10),
                DueDate = DateTime.Now.AddDays(4),
                IsReturned = false
            },
            new BorrowingRecord
            {
                BookId = books[0].Id,
                BorrowerName = "Jane Smith",
                BorrowerEmail = "jane.smith@example.com",
                BorrowDate = DateTime.Now.AddDays(-20),
                DueDate = DateTime.Now.AddDays(-6),
                ReturnDate = DateTime.Now.AddDays(-5),
                IsReturned = true
            },
            
            // Borrowings for Design Patterns
            new BorrowingRecord
            {
                BookId = books[1].Id,
                BorrowerName = "Bob Johnson",
                BorrowerEmail = "bob.johnson@example.com",
                BorrowDate = DateTime.Now.AddDays(-15),
                DueDate = DateTime.Now.AddDays(-1),
                IsReturned = false
            },
            new BorrowingRecord
            {
                BookId = books[1].Id,
                BorrowerName = "Alice Brown",
                BorrowerEmail = "alice.brown@example.com",
                BorrowDate = DateTime.Now.AddDays(-30),
                DueDate = DateTime.Now.AddDays(-16),
                ReturnDate = DateTime.Now.AddDays(-14),
                IsReturned = true
            },
            
            // Borrowings for The Pragmatic Programmer
            new BorrowingRecord
            {
                BookId = books[2].Id,
                BorrowerName = "Charlie Wilson",
                BorrowerEmail = "charlie.wilson@example.com",
                BorrowDate = DateTime.Now.AddDays(-5),
                DueDate = DateTime.Now.AddDays(9),
                IsReturned = false
            },
            new BorrowingRecord
            {
                BookId = books[2].Id,
                BorrowerName = "David Miller",
                BorrowerEmail = "david.miller@example.com",
                BorrowDate = DateTime.Now.AddDays(-25),
                DueDate = DateTime.Now.AddDays(-11),
                ReturnDate = DateTime.Now.AddDays(-10),
                IsReturned = true
            }
        };

        await dbContext.BorrowingRecords.AddRangeAsync(borrowings);
        await dbContext.SaveChangesAsync();
    }
}