using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibraryManagement.Core.Entities;
using LibraryManagement.Core.DTOs;


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

    /// <summary>
    /// Gets filtered, sorted and paginated books with their borrowing records
    /// </summary>
    /// <param name="title">Optional title filter</param>
    /// <param name="author">Optional author filter</param>
    /// <param name="genre">Optional genre filter</param>
    /// <param name="minYear">Optional minimum publication year filter</param>
    /// <param name="maxYear">Optional maximum publication year filter</param>
    /// <param name="pageNumber">Page number for pagination (1-based)</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="sortBy">Property to sort by (title, author, year, etc.)</param>
    /// <param name="ascending">Sort direction (true for ascending, false for descending)</param>
    /// <returns>A tuple containing books and total count</returns>
    Task<(IEnumerable<Book> Books, int TotalCount)> GetFilteredBooksAsync(
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
    Task<Book?> UpdateBookAsync(int id, BookUpdateDto updateDto);
}