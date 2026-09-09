using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EquipmentBorrowing.Application.Services;

namespace EquipmentBorrowing.Desktop.ViewModels;

public partial class EquipmentViewModel : ViewModelBase
{
    private readonly BorrowEquipmentService _borrowService;

    [ObservableProperty]
    private object? _selectedEquipment;

    [ObservableProperty]
    private string _studentId = string.Empty;

    [ObservableProperty]
    private DateTimeOffset? _returnDate;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public ObservableCollection<object> EquipmentList { get; set; } = new ObservableCollection<object>();

    public EquipmentViewModel(BorrowEquipmentService borrowService)
    {
        _borrowService = borrowService;
    }

    [RelayCommand]
    private async Task BorrowAsync()
    {
        if (SelectedEquipment == null)
        {
            StatusMessage = "Error: Please select equipment from the list.";
            return;
        }
        if (string.IsNullOrWhiteSpace(StudentId))
        {
            StatusMessage = "Error: Student ID cannot be empty.";
            return;
        }
        if (ReturnDate == null)
        {
            StatusMessage = "Error: Please select a return date.";
            return;
        }

        StatusMessage = "Processing...";

        try
        {
            
            StatusMessage = "Successfully borrowed equipment!";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed: {ex.Message}";
        }
    }
}