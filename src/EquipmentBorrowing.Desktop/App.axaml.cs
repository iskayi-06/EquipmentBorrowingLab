using EquipmentBorrowing.Application.Interfaces;
using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

// Desktop Imports
using EquipmentBorrowing.Desktop.ViewModels;
using EquipmentBorrowing.Desktop.Views;

// Application and Domain Imports
using EquipmentBorrowing.Application.Services;


// Note: If 'InMemoryEquipmentRepository' is red, click it and press Ctrl + . to import your specific Lab 1 Infrastructure folder!
using EquipmentBorrowing.Infrastructure;
using EquipmentBorrowing.Application.Interfaces;
using EquipmentBorrowing.Infrastructure.Repositories;

namespace EquipmentBorrowing.Desktop;

public partial class App : Avalonia.Application
{
    // Named ServiceProvider to avoid colliding with your Application.Services folder!
    public IServiceProvider? ServiceProvider { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();

        // 1. Register Repositories 
        services.AddSingleton<IEquipmentRepository, InMemoryEquipmentRepository>();
        services.AddSingleton<IBorrowingRepository, InMemoryBorrowingRepository>();

        // 2. Register Application Services
        services.AddTransient<BorrowEquipmentService>();
        services.AddTransient<ReturnEquipmentService>();

        // 3. Register ViewModels
        services.AddTransient<MainViewModel>();
        services.AddTransient<EquipmentViewModel>();
        services.AddTransient<BorrowingsViewModel>();

        // 4. Build the provider
        ServiceProvider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = ServiceProvider!.GetRequiredService<MainViewModel>()
            };
        }
       

        base.OnFrameworkInitializationCompleted();
    }
}