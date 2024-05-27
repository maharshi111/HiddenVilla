using HiiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using Models1;

namespace HiiddenVilla_Client.Pages.Authentication
{
    public partial class Register
    {
        private UserRequestDTO UserForRegistration = new UserRequestDTO();
        public bool IsProcessing { get; set; } = false;
        public bool ShowRegistrationError { get; set; }
        public IEnumerable<string> Errors { get; set; }
        [Inject]
        public IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        private async Task RegisterUser()
        {
            ShowRegistrationError = false;
            IsProcessing = true;
            var result = await authenticationService.RegisterUser(UserForRegistration);
            if (result.IsRegistrationSuccessful)
            {
                IsProcessing = false;
                navigationManager.NavigateTo("/login");
            }
            else
            {
                IsProcessing = false;
                Errors = result.Errors;
                ShowRegistrationError = true;
            }

        }

    }
}
