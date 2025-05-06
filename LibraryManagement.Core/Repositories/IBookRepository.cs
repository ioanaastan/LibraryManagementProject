using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories;

public interface IBookRepository
{
    /// <summary>
    /// Gets all books with their associated borrowing records
    /// </summary>
    /// <returns>A collection of books with their borrowing records</returns>
    Task<IEnumerable<Book>> GetAllBooksWithBorrowingsAsync();

    /// <summary>
    /// Gets a specific book with its associated borrowing records by ID
    /// </summary>
    /// <param name="id">The ID of the book to retrieve</param>
    /// <returns>The book with its borrowing records, or null if not found</returns>
    Task<Book?> GetBookWithBorrowingsByIdAsync(int id);
}