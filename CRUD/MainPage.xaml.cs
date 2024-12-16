using CRUD.Models;
using CRUD.Pages;
using CRUD.Services;

namespace CRUD
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

       private void OnButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserListPage());
        }
    }

}
