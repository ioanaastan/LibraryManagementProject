using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibraryManagement.Core;
using LibraryManagement.Core.DTOs;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookWithBorrowingsDto>> GetAllBooksWithBorrowingsAsync()
        {
            var books = await _bookRepository.GetAllBooksWithBorrowingsAsync();
            return books.Select(MapToBookWithBorrowingsDto);
        }

        public async Task<BookWithBorrowingsDto?> GetBookWithBorrowingsByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookWithBorrowingsByIdAsync(id);
            return book != null ? MapToBookWithBorrowingsDto(book) : null;
        }

        private BookWithBorrowingsDto MapToBookWithBorrowingsDto(Book book)
        {
            return new BookWithBorrowingsDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                Publisher = book.Publisher,
                Genre = book.Genre,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies,
                BorrowingRecords = book.BorrowingRecords?.Select(borrowing => new BorrowingRecordDto
                {
                    Id = borrowing.Id,
                    BookId = borrowing.BookId,
                    BorrowerName = borrowing.BorrowerName,
                    BorrowerEmail = borrowing.BorrowerEmail,
                    BorrowDate = borrowing.BorrowDate,
                    DueDate = borrowing.DueDate,
                    ReturnDate = borrowing.ReturnDate,
                    IsReturned = borrowing.IsReturned
                }).ToList() ?? new List<BorrowingRecordDto>()
            };
        }

        public async Task<(IEnumerable<BookWithBorrowingsDto> Books, int TotalCount)> GetFilteredBooksAsync(
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
            var (books, totalCount) = await _bookRepository.GetFilteredBooksAsync(
                title, author, genre, minYear, maxYear, pageNumber, pageSize, sortBy, ascending);

            return (books.Select(MapToBookWithBorrowingsDto), totalCount);
        }

        public async Task<BookWithBorrowingsDto?> UpdateBookAsync(int id, BookUpdateDto updateDto)
        {
            var book = await _bookRepository.UpdateBookAsync(id, updateDto);

            if (book == null)
                throw new BookNotFoundException(id);

            return MapToBookWithBorrowingsDto(book);
        }
    }
}