using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRUD.Models;
using CRUD.Pages.Add;
using CRUD.Pages.Edit;
using CRUD.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CRUD.Pages;

public partial class UserListPageViewModel : ObservableObject
{
    private readonly UserService _service;

    [ObservableProperty]
    private ObservableCollection<User> peoples = new();

    [ObservableProperty]
    private User people;

    public IAsyncRelayCommand LoadPeopleCommand { get; }
    public IAsyncRelayCommand AddUserCommand { get; }
    public ICommand EditUserCommand { get; }
    public IAsyncRelayCommand<int> DeleteUserCommand { get; }

    public UserListPageViewModel()
    {
        _service = new UserService();
        LoadPeopleCommand = new AsyncRelayCommand(LoadPeopleAsync);
        AddUserCommand = new AsyncRelayCommand(NavigateToAddUserPageAsync);
        EditUserCommand = new RelayCommand<User>(EditUser);
        DeleteUserCommand = new AsyncRelayCommand<int>(DeleteUserAsync);
        LoadPeopleAsync();
    }

    private async Task LoadPeopleAsync()
    {
        var users = await _service.GetUsersAsync();
        Peoples = new ObservableCollection<User>(users);
    }


    private async Task NavigateToAddUserPageAsync()
    {
        await Shell.Current.GoToAsync("///AddUserPage");
    }

    private async void EditUser(User user)
    {
        await Shell.Current.GoToAsync($"EditUserPage?userId={user.id}");
    }

  

    private async Task DeleteUserAsync(int userId)
    {
        var isDeleted = await _service.DeleteUserAsync(userId);

        if (isDeleted)
        {
            var userToDelete = Peoples.FirstOrDefault(u => u.id == userId);
            if (userToDelete != null)
            {
                Peoples.Remove(userToDelete);
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Failed to delete the user.", "OK");
        }
    }
}