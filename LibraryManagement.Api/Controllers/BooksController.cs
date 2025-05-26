using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Core;
using LibraryManagement.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Gets all books with their associated borrowing records
        /// </summary>
        /// <returns>A collection of books with their borrowing records</returns>
        /// <response code="200">Returns the list of books with borrowing records</response>
        [HttpGet("all")]  // <- Add route template here
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookWithBorrowingsDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksWithBorrowingsAsync();
            return Ok(books);
        }

        /// <summary>
        /// Gets a specific book with its borrowing records by ID
        /// </summary>
        /// <param name="id">The ID of the book to retrieve</param>
        /// <returns>The book with its borrowing records</returns>
        /// <response code="200">Returns the book with its borrowing records</response>
        /// <response code="404">If the book is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookWithBorrowingsDto>> GetBook(int id)
        {
            var book = await _bookService.GetBookWithBorrowingsByIdAsync(id);

            if (book == null)
            {
                return NotFound($"Book with ID {id} not found");
            }

            return Ok(book);
        }

        /// <summary>
        /// Gets filtered books with pagination and sorting
        /// </summary>
        [HttpGet("filter")]  // <- Add route template here
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<BookWithBorrowingsDto>>> GetBooks(
            [FromQuery] string? title = null,
            [FromQuery] string? author = null,
            [FromQuery] string? genre = null,
            [FromQuery] int? minYear = null,
            [FromQuery] int? maxYear = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool ascending = true)
        {
            var (books, totalCount) = await _bookService.GetFilteredBooksAsync(
                title, author, genre, minYear, maxYear, pageNumber, pageSize, sortBy, ascending);

            var result = new PagedResult<BookWithBorrowingsDto>
            {
                Items = books,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(result);
        }

        /// <summary>
        /// Updates a book by ID
        /// </summary>
        /// <param name="id">The ID of the book to update</param>
        /// <param name="updateDto">The update information</param>
        /// <returns>The updated book</returns>
        /// <response code="200">Returns the updated book</response>
        /// <response code="404">If the book is not found</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookWithBorrowingsDto>> UpdateBook(int id, [FromBody] BookUpdateDto updateDto)
        {
            var updatedBook = await _bookService.UpdateBookAsync(id, updateDto);
            return Ok(updatedBook);
        }
    }
}