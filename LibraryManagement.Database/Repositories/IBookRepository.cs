using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;


using Microsoft.EntityFrameworkCore;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;

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
}