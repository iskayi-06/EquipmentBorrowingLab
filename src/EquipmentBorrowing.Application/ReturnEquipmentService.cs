using System.Threading.Tasks;
using EquipmentBorrowing.Application.Interfaces;
using EquipmentBorrowing.Domain;

namespace EquipmentBorrowing.Application.Services;

public class ReturnEquipmentService
{
    private readonly IEquipmentRepository _equipmentRepo;

    public ReturnEquipmentService(IEquipmentRepository equipmentRepo)
    {
        _equipmentRepo = equipmentRepo;
    }
    public async Task ReturnAsync(Borrowing borrowing)
    {
        if (borrowing == null) return;

        borrowing.Equipment.IsAvailable = true;
        borrowing.Student.ActiveBorrowingsCount--;
        borrowing.Status = BorrowingStatus.Returned;

        await _equipmentRepo.UpdateAsync(borrowing.Equipment);
    }
}