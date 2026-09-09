using CommunityToolkit.Mvvm.ComponentModel;
using EquipmentBorrowing.Application.Services;

namespace EquipmentBorrowing.Desktop.ViewModels;

public partial class EquipmentViewModel : ViewModelBase
{
    private readonly BorrowEquipmentService _borrowService;

    public EquipmentViewModel(BorrowEquipmentService borrowService)
    {
        _borrowService = borrowService;
    }
}