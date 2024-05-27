using HiiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using Models1;
using System.Web;

namespace HiiddenVilla_Client.Pages.Authentication
{
    public partial class Login
    {
        private AuthenticationDTO UserForAuthentication = new AuthenticationDTO();
        public bool IsProcessing { get; set; } = false;
        public bool ShowAuthenticationError { get; set; }

        public string ReturnUrl { get; set; }
        public string Errors { get; set; }
        [Inject]
        public IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        private async Task LoginUser()
        {
            ShowAuthenticationError = false;
            IsProcessing = true;
            var result = await authenticationService.Login(UserForAuthentication);
            if (result.IsAuthSuccessful)
            {
                IsProcessing = false;
                var absoluteUri = new Uri(navigationManager.Uri);
                var queryparm = HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnUrl = queryparm["returnUrl"];

                navigationManager.NavigateTo("/");
            }
            else
            {
                IsProcessing = false;
                Errors = result.ErrorMessage;
                ShowAuthenticationError = true;
            }

        }
    }
}
