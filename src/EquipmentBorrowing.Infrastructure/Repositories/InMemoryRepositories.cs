using EquipmentBorrowing.Application.Interfaces;
using EquipmentBorrowing.Domain;

namespace EquipmentBorrowing.Infrastructure.Repositories;

public class InMemoryStudentRepository : IStudentRepository
{
    private readonly List<Student> _students = new();

    // Helper method to add test data
    public void Seed(Student student) => _students.Add(student);

    public Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_students.FirstOrDefault(s => s.Id == id));
    }
}

public class InMemoryEquipmentRepository : IEquipmentRepository
{
    private readonly List<Equipment> _equipment = new();

    // Helper method to add test data
    public void Seed(Equipment equipment) => _equipment.Add(equipment);

    public Task<Equipment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_equipment.FirstOrDefault(e => e.Id == id));
    }

    public Task UpdateAsync(Equipment equipment, CancellationToken cancellationToken = default)
    {
        // In-memory lists update automatically via reference, but we must satisfy the interface
        return Task.CompletedTask;
    }
}

public class InMemoryBorrowingRepository : IBorrowingRepository
{
    private readonly List<Borrowing> _borrowings = new();

    public Task AddAsync(Borrowing borrowing, CancellationToken cancellationToken = default)
    {
        _borrowings.Add(borrowing);
        return Task.CompletedTask;
    }
}