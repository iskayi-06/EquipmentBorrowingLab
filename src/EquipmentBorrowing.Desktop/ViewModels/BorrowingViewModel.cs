using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EquipmentBorrowing.Application.Interfaces;
using EquipmentBorrowing.Application.Services;
using EquipmentBorrowing.Domain;
using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using EquipmentBorrowing.Domain;
using EquipmentBorrowing.Application.Interfaces;
using EquipmentBorrowing.Application.Services;
namespace EquipmentBorrowing.Desktop.ViewModels;

public partial class BorrowingsViewModel : ViewModelBase
{
    private readonly ReturnEquipmentService _returnService;
    private readonly IBorrowingRepository _borrowingRepo; // Added repo

    [ObservableProperty]
    private Borrowing? _selectedBorrowing;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public ObservableCollection<Borrowing> BorrowingsList { get; set; } = new();

    public BorrowingsViewModel(ReturnEquipmentService returnService, IBorrowingRepository borrowingRepo)
    {
        _returnService = returnService;
        _borrowingRepo = borrowingRepo;
        
        Refresh();
    }

    [RelayCommand]
    private void Refresh()
    {
        BorrowingsList.Clear();
        var items = _borrowingRepo.GetAll();
        
        foreach (var item in items)
        {
            // ONLY add it to the UI if it is still Active!
            if (item.Status == BorrowingStatus.Active)
            {
                BorrowingsList.Add(item);
            }
        }
}

    [RelayCommand]
    private async Task ReturnAsync()
    {
        if (SelectedBorrowing == null)
        {
            StatusMessage = "Error: Please select a record to return.";
            return;
        }

        try
        {
            await _returnService.ReturnAsync(SelectedBorrowing);
            StatusMessage = "Successfully returned equipment!";
            BorrowingsList.Remove(SelectedBorrowing);
            SelectedBorrowing = null;
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed: {ex.Message}";
        }
    }
}