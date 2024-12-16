using CRUD.Pages;
using CRUD.Pages.Edit;

namespace CRUD
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("UserListPage", typeof(UserListPage));
            Routing.RegisterRoute("EditUserPage", typeof(EditUserPage));
        }
    }
}
