using CRUD.Models;

namespace CRUD.Pages;

public partial class UserListPage : ContentPage
{
    private readonly UserListPageViewModel userListPageViewModel;

    public UserListPage()
    {
        InitializeComponent();
        userListPageViewModel = new UserListPageViewModel();
        BindingContext = userListPageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await userListPageViewModel.LoadPeopleCommand.ExecuteAsync(null);

    }
}