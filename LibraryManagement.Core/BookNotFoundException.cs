using System;

namespace LibraryManagement.Core;

public class BookNotFoundException : Exception
{
    public int BookId { get; }

    public BookNotFoundException(int bookId)
        : base($"Book with ID {bookId} not found")
    {
        BookId = bookId;
    }
}