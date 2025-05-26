using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibraryManagement.Core.DTOs;

namespace LibraryManagement.Core;

public interface IBookService
{
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

    /// <summary>
    /// Gets filtered books with their associated borrowing records
    /// </summary>
    /// <returns>A tuple containing books and total count</returns>
    Task<(IEnumerable<BookWithBorrowingsDto> Books, int TotalCount)> GetFilteredBooksAsync(
        string? title = null,
        string? author = null,
        string? genre = null,
        int? minYear = null,
        int? maxYear = null,
        int pageNumber = 1,
        int pageSize = 10,
        string? sortBy = null,
        bool ascending = true);

    /// <summary>
    /// Updates a book with the provided information
    /// </summary>
    /// <param name="id">The ID of the book to update</param>
    /// <param name="updateDto">The update information</param>
    /// <returns>The updated book, or null if not found</returns>
    Task<BookWithBorrowingsDto?> UpdateBookAsync(int id, BookUpdateDto updateDto);
}