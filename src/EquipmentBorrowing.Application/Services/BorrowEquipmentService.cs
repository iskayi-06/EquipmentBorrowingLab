using EquipmentBorrowing.Application.Interfaces;
using EquipmentBorrowing.Domain;

namespace EquipmentBorrowing.Application.Services;

public class BorrowEquipmentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IBorrowingRepository _borrowingRepository;

    public BorrowEquipmentService(
        IStudentRepository studentRepository,
        IEquipmentRepository equipmentRepository,
        IBorrowingRepository borrowingRepository)
    {
        _studentRepository = studentRepository;
        _equipmentRepository = equipmentRepository;
        _borrowingRepository = borrowingRepository;
    }

    public async Task<Borrowing?> BorrowAsync(int studentId, int equipmentId, int daysToBorrow)
    {
        var student = await _studentRepository.GetByIdAsync(studentId);
        if (student == null || !student.IsAllowedToBorrow || student.ActiveBorrowingsCount >= student.MaxAllowedBorrowings)
        {
            Console.WriteLine("Borrowing failed: Student is invalid, not allowed, or reached max limit.");
            return null;
        }

        var equipment = await _equipmentRepository.GetByIdAsync(equipmentId);
        if (equipment == null || !equipment.IsAvailable)
        {
            Console.WriteLine("Borrowing failed: Equipment does not exist or is currently unavailable.");
            return null;
        }

        var borrowing = new Borrowing
        {
            Id = new Random().Next(1, 1000), 
            Student = student,
            Equipment = equipment,
            DateBorrowed = DateTime.Now,
            ExpectedReturnDate = DateTime.Now.AddDays(daysToBorrow),
            Status = BorrowingStatus.Active
        };

        student.ActiveBorrowingsCount++;
        equipment.IsAvailable = false;

        await _equipmentRepository.UpdateAsync(equipment);
        await _borrowingRepository.AddAsync(borrowing);

        Console.WriteLine($"Success: {student.Name} borrowed {equipment.Name}. Expected return by {borrowing.ExpectedReturnDate.ToShortDateString()}.");
        return borrowing;
    }
}