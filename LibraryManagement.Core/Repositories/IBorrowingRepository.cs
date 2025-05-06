using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories;

public interface IBorrowingRepository
{
    /// <summary>
    /// Gets all borrowing records for a specific book
    /// </summary>
    /// <param name="bookId">The ID of the book</param>
    /// <returns>Collection of borrowing records</returns>
    Task<IEnumerable<BorrowingRecord>> GetBorrowingsByBookIdAsync(int bookId);

    /// <summary>
    /// Gets all active (not returned) borrowings
    /// </summary>
    /// <returns>Collection of active borrowing records</returns>
    Task<IEnumerable<BorrowingRecord>> GetActiveBorrowingsAsync();
}