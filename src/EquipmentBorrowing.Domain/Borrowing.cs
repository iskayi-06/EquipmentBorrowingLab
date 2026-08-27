using System;

namespace EquipmentBorrowing.Domain;

public class Borrowing
{
    public int Id { get; set; }
    public required Student Student { get; set; }
    public required Equipment Equipment { get; set; }
    public DateTime DateBorrowed { get; set; }
    public DateTime ExpectedReturnDate { get; set; }
    public BorrowingStatus Status { get; set; } = BorrowingStatus.Active;
}
