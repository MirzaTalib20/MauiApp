namespace CRUD.Pages.Edit;

[QueryProperty(nameof(UserId), "userId")]
public partial class EditUserPage : ContentPage
{
    public int UserId { get; set; }

    public EditUserPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BindingContext = new EditUserPageViewModel(UserId);
    }
}
