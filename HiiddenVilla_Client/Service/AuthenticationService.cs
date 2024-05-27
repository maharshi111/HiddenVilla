using Blazored.LocalStorage;
using Common;
using HiiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Models1;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;


namespace HiiddenVilla_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<AuthenticationResponseDTO> Login(AuthenticationDTO userFromAuthentication)
        {
            var content = JsonConvert.SerializeObject(userFromAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/SignIn", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(SD.Local_Token, result.Token);
                await _localStorage.SetItemAsync(SD.Local_UserDetails, result.userDTO);
                ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new AuthenticationResponseDTO { IsAuthSuccessful = true };

            }
            else
            {
                return result;
            }

        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(SD.Local_Token);
            await _localStorage.RemoveItemAsync(SD.Local_UserDetails);
            ((AuthStateProvider)_authStateProvider).NotifyUserLoggedOut();
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegisteration)
        {
            var content = JsonConvert.SerializeObject(userForRegisteration);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/SignUp", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegistrationResponseDTO>(contentTemp);
            if (response.IsSuccessStatusCode)
            {
                return new RegistrationResponseDTO { IsRegistrationSuccessful = true };
            }
            else
            {
                return result;
            }


        }
    }
}
