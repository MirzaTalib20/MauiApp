using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRUD.Models;
using CRUD.Services;

namespace CRUD.Pages.Edit;
public partial class EditUserPageViewModel : ObservableObject
{

    private readonly UserService _service;

    private int _userId;

    [ObservableProperty]
    private User selectedUser;

    public IAsyncRelayCommand LoadUserCommand { get; }
    public Command SaveUserCommand { get; }

    public EditUserPageViewModel(int userId)
    {
        _service = new UserService();
        _userId = userId;
        LoadUserCommand = new AsyncRelayCommand(LoadUserAsync);
        SaveUserCommand = new Command(async () => await SaveUserAsync());
        LoadUserAsync();
    }

    private async Task LoadUserAsync()
    {
        if (_userId <= 0)
            return;

        var user = await _service.GetUserByIdAsync(_userId);
        if (user != null)
        {
            SelectedUser = user;
        }
    }

    private async Task SaveUserAsync()
    {
        if (string.IsNullOrWhiteSpace(SelectedUser.firstname) || string.IsNullOrWhiteSpace(SelectedUser.lastname))
        {
            // Show validation message or simply return
            return;
        }

        // Call the service to update the user
        await _service.UpdateUserAsync(SelectedUser);

        // Navigate back to the user list
        await Shell.Current.GoToAsync("/UserListPage");
    }
}