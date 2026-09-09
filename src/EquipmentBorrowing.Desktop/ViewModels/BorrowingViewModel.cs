using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EquipmentBorrowing.Application.Services; 

namespace EquipmentBorrowing.Desktop.ViewModels;

public partial class BorrowingsViewModel : ViewModelBase
{
    private readonly ReturnEquipmentService _returnService;
    [ObservableProperty]
    private object? _selectedBorrowing;

    [ObservableProperty]
private string _statusMessage = string.Empty;

    public ObservableCollection<object> BorrowingsList {get; set; } = new ObservableCollection<object>();


    public BorrowingsViewModel(ReturnEquipmentService returnService)
    {
        _returnService = returnService;
    }
    [RelayCommand]
    private async Task ReturnAsync()
    {
        if (SelectedBorrowing == null)
        {
            StatusMessage = "Error: Please select a record to return.";
            return;
        }
        StatusMessage = "Processing return...";
        
        try
        {
            StatusMessage = "Successfully returned equipment!";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed: {ex.Message}";
        }
    }
}