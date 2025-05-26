using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Database.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllBooksWithBorrowingsAsync()
    {
        return await _context.Books
            .Include(b => b.BorrowingRecords)
            .ToListAsync();
    }

    public async Task<Book?> GetBookWithBorrowingsByIdAsync(int id)
    {
        return await _context.Books
            .Include(b => b.BorrowingRecords)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<(IEnumerable<Book> Books, int TotalCount)> GetFilteredBooksAsync(
        string? title = null,
        string? author = null,
        string? genre = null,
        int? minYear = null,
        int? maxYear = null,
        int pageNumber = 1,
        int pageSize = 10,
        string? sortBy = null,
        bool ascending = true)
    {
        // Start with all books
        IQueryable<Book> query = _context.Books.Include(b => b.BorrowingRecords);

        // Apply filters
        if (!string.IsNullOrEmpty(title))
            query = query.Where(b => b.Title.Contains(title));

        if (!string.IsNullOrEmpty(author))
            query = query.Where(b => b.Author.Contains(author));

        if (!string.IsNullOrEmpty(genre))
            query = query.Where(b => b.Genre.Contains(genre));

        if (minYear.HasValue)
            query = query.Where(b => b.PublicationYear >= minYear.Value);

        if (maxYear.HasValue)
            query = query.Where(b => b.PublicationYear <= maxYear.Value);

        // Get total count before pagination
        int totalCount = await query.CountAsync();

        // Apply sorting
        if (!string.IsNullOrEmpty(sortBy))
        {
            switch (sortBy.ToLower())
            {
                case "title":
                    query = ascending
                        ? query.OrderBy(b => b.Title)
                        : query.OrderByDescending(b => b.Title);
                    break;
                case "author":
                    query = ascending
                        ? query.OrderBy(b => b.Author)
                        : query.OrderByDescending(b => b.Author);
                    break;
                case "year":
                    query = ascending
                        ? query.OrderBy(b => b.PublicationYear)
                        : query.OrderByDescending(b => b.PublicationYear);
                    break;
                case "isbn":
                    query = ascending
                        ? query.OrderBy(b => b.ISBN)
                        : query.OrderByDescending(b => b.ISBN);
                    break;
                case "publisher":
                    query = ascending
                        ? query.OrderBy(b => b.Publisher)
                        : query.OrderByDescending(b => b.Publisher);
                    break;
                case "genre":
                    query = ascending
                        ? query.OrderBy(b => b.Genre)
                        : query.OrderByDescending(b => b.Genre);
                    break;
                case "totalcopies":
                    query = ascending
                        ? query.OrderBy(b => b.TotalCopies)
                        : query.OrderByDescending(b => b.TotalCopies);
                    break;
                case "availablecopies":
                    query = ascending
                        ? query.OrderBy(b => b.AvailableCopies)
                        : query.OrderByDescending(b => b.AvailableCopies);
                    break;
                default:
                    // Default sorting if invalid option provided
                    query = query.OrderBy(b => b.Id);
                    break;
            }
        }
        else
        {
            // Default sorting if no option provided
            query = query.OrderBy(b => b.Id);
        }

        // Apply pagination
        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        // Execute query
        var books = await query.ToListAsync();

        return (books, totalCount);
    }

    public async Task<Book?> UpdateBookAsync(int id, BookUpdateDto updateDto)
    {
        var book = await _context.Books
            .Include(b => b.BorrowingRecords)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
            return null;

        // Update only the properties that are provided
        if (updateDto.Title != null)
            book.Title = updateDto.Title;

        if (updateDto.Author != null)
            book.Author = updateDto.Author;

        if (updateDto.ISBN != null)
            book.ISBN = updateDto.ISBN;

        if (updateDto.PublicationYear.HasValue)
            book.PublicationYear = updateDto.PublicationYear.Value;

        if (updateDto.Publisher != null)
            book.Publisher = updateDto.Publisher;

        if (updateDto.Genre != null)
            book.Genre = updateDto.Genre;

        if (updateDto.TotalCopies.HasValue)
        {
            // Calculate the difference for available copies
            int difference = updateDto.TotalCopies.Value - book.TotalCopies;
            book.TotalCopies = updateDto.TotalCopies.Value;
            book.AvailableCopies += difference;

            // Ensure available copies doesn't go negative
            if (book.AvailableCopies < 0)
                book.AvailableCopies = 0;
        }

        await _context.SaveChangesAsync();
        return book;
    }
}