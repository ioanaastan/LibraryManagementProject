using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibraryManagement.Core.DTOs;

namespace LibraryManagement.Core;

public interface IBookService
{
    /// <summary>
    /// Gets all books with their associated borrowing records
    /// </summary>
    /// <returns>A collection of books with borrowings</returns>
    Task<IEnumerable<BookWithBorrowingsDto>> GetAllBooksWithBorrowingsAsync();

    /// <summary>
    /// Gets a specific book with its borrowing records by id
    /// </summary>
    /// <param name="id">The id of the book to retrieve</param>
    /// <returns>The book with borrowings or null if not found</returns>
    Task<BookWithBorrowingsDto?> GetBookWithBorrowingsByIdAsync(int id);
}