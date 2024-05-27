using HiiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;

namespace HiiddenVilla_Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await authenticationService.Logout();
            navigationManager.NavigateTo("/");
        }

    }
}
