using Models1;

namespace HiiddenVilla_Client.Service.IService
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegisteration);

        Task<AuthenticationResponseDTO> Login(AuthenticationDTO userFromAuthentication);

        Task Logout();
    }
}
