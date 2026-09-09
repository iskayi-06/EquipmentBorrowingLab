using EquipmentBorrowing.Application.Interfaces;
using EquipmentBorrowing.Domain;

namespace EquipmentBorrowing.Infrastructure.Repositories;

public class InMemoryStudentRepository : IStudentRepository
{
    private readonly List<Student> _students = new();


    public void Seed(Student student) => _students.Add(student);

    public Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_students.FirstOrDefault(s => s.Id == id));
    }
}

public class InMemoryEquipmentRepository : IEquipmentRepository
{
    private readonly List<Equipment> _equipment = new();

public InMemoryEquipmentRepository()
{
    // Add some dummy data so the UI isn't empty
    _equipment.Add(new Equipment { Id = 1, Name = "Oscilloscope", IsAvailable = true });
    _equipment.Add(new Equipment { Id = 2, Name = "Digital Multimeter", IsAvailable = true });
    _equipment.Add(new Equipment { Id = 3, Name = "Soldering Station", IsAvailable = true });
}

    public void Seed(Equipment equipment) => _equipment.Add(equipment);

    public Task<Equipment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_equipment.FirstOrDefault(e => e.Id == id));
    }

    public Task UpdateAsync(Equipment equipment, CancellationToken cancellationToken = default)
    {
        
        return Task.CompletedTask;
    }
    public IEnumerable<Equipment> GetAll()
{
    return _equipment;
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