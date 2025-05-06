using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Entities;

public class BorrowingRecord
{
    // Primary key
    public int Id { get; set; }

    // Foreign key to Book
    public int BookId { get; set; }

    // Borrowing details
    public string BorrowerName { get; set; } = string.Empty;
    public string BorrowerEmail { get; set; } = string.Empty;
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }  // Nullable because book might not be returned yet
    public bool IsReturned { get; set; }

    // Navigation property for the many-to-one relationship
    public Book Book { get; set; } = null!;
}