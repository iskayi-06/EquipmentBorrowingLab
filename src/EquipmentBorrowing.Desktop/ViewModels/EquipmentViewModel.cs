using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EquipmentBorrowing.Application.Services;
using EquipmentBorrowing.Application.Interfaces;
using EquipmentBorrowing.Domain;

namespace EquipmentBorrowing.Desktop.ViewModels;

public partial class EquipmentViewModel : ViewModelBase
{
    private readonly BorrowEquipmentService _borrowService;
    private readonly IEquipmentRepository _equipmentRepo;

    [ObservableProperty]
    private Equipment? _selectedEquipment;

    [ObservableProperty]
    private string _studentId = string.Empty;

    [ObservableProperty]
    private DateTimeOffset? _returnDate;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

public ObservableCollection<Equipment> EquipmentList { get; set; } = new ObservableCollection<Equipment>();
    public EquipmentViewModel(BorrowEquipmentService borrowService, IEquipmentRepository equipmentRepo)
    {
        _borrowService = borrowService;
        _equipmentRepo = equipmentRepo;
        
        LoadEquipment();
    }

    private void LoadEquipment()
    {
        EquipmentList.Clear();
        
        // Fetch data from your Lab 1 repository
        var items = _equipmentRepo.GetAll(); 
        
        foreach (var item in items)
        {
            EquipmentList.Add(item);
        }
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