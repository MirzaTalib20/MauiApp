using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRUD.Models;
using CRUD.Services;

namespace CRUD.Pages.Add;

public partial class AddUserPageViewModel : ObservableObject
{
    private readonly UserService _service;

    [ObservableProperty]
    private User newUser = new();
    public IAsyncRelayCommand AddPersonCommand { get; }
    public IAsyncRelayCommand NavigateBackCommand { get; }
    public AddUserPageViewModel()
    {
        _service = new UserService();
        AddPersonCommand = new AsyncRelayCommand(AddUserAsync);
        NavigateBackCommand = new AsyncRelayCommand(GoBackAsync);
    }
 
    private async Task AddUserAsync()
    {
        if (string.IsNullOrWhiteSpace(NewUser.firstname) || string.IsNullOrWhiteSpace(NewUser.lastname))
        {
            await Application.Current.MainPage.DisplayAlert("Validation Error", "First name and last name are required.", "OK");
            return;
        }

        var isAdded = await _service.AddUserAsync(NewUser);

        if (isAdded)
        {
            await Application.Current.MainPage.DisplayAlert("Success", "User added successfully!", "OK");

            await Shell.Current.GoToAsync("/UserListPage");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Failed to add the user. Please try again.", "OK");
        }
    }
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("/UserListPage");
    }
}