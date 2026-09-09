using CommunityToolkit.Mvvm.ComponentModel;
using EquipmentBorrowing.Application.Services; 

namespace EquipmentBorrowing.Desktop.ViewModels;

public partial class BorrowingsViewModel : ViewModelBase
{
    private readonly ReturnEquipmentService _returnService;

    public BorrowingsViewModel(ReturnEquipmentService returnService)
    {
        _returnService = returnService;
    }
}