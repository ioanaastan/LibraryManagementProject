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
        [HttpGet]
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
    }
}