using Common;
using DataAccess.Data;
using HiddenVilla_API.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models1;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HiddenVilla_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly APISettings _aPISettings;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManger, RoleManager<IdentityRole> roleManager, IOptions<APISettings> options)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManger = userManger;
            _aPISettings = options.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] UserRequestDTO userRequestDTO)
        {
            if (userRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = new ApplicationUser
            {
                UserName = userRequestDTO.Email,
                Email = userRequestDTO.Email,
                Name = userRequestDTO.Name,
                PhoneNumber = userRequestDTO.PhoneNo,
                EmailConfirmed = true
            };
            var result = await _userManger.CreateAsync(user, userRequestDTO.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDTO { Errors = errors, IsRegistrationSuccessful = false });
            }
            var roleresult = await _userManger.AddToRoleAsync(user, SD.Role_Customer);
            if (!roleresult.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDTO { Errors = errors, IsRegistrationSuccessful = false });
            }
            return StatusCode(201);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] AuthenticationDTO authenticationDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(authenticationDTO.UserName, authenticationDTO.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManger.FindByNameAsync(authenticationDTO.UserName);
                if (user == null)
                {
                    return Unauthorized(new AuthenticationResponseDTO
                    {
                        IsAuthSuccessful = false,
                        ErrorMessage = "Invalid Authentication"
                    });
                }

                //if reach at this at point every thing is valid
                var signinCredentials = GetSigningCredentials();
                var claims = await GetClaim(user);
                var tokenOptions = new JwtSecurityToken(
                        issuer: _aPISettings.ValidIssuer,
                        audience: _aPISettings.ValidAudience,
                        claims: claims,
                        expires: DateTime.Now.AddDays(30),
                        signingCredentials: signinCredentials
                    );
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new AuthenticationResponseDTO
                {
                    IsAuthSuccessful = true,
                    Token = token,
                    userDTO = new UserDTO
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNo = user.PhoneNumber

                    }
                }); ;
            }
            else
            {
                return Unauthorized(new AuthenticationResponseDTO
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "Inavalid Authentication"
                });
            }
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_aPISettings.SeceretKey));
            // var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("\"251dK9_??UZ1Kt?3GMdy@fjvS7PO:}W(sw7aJa3,;lb~%+/<eeA$3e-V\"W)kF>X\","));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        // the above method is used to generate sign in credential
        private async Task<List<Claim>> GetClaim(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id",user.Id),
            };
            var roles = await _userManger.GetRolesAsync(await _userManger.FindByEmailAsync(user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

            }
            return claims;
        }
        // purpouse of creating GetClaim
        //During user authentication, the claims generated by this method are used to create the user's identity. 
        //These claims are then encoded into an authentication token (e.g., JWT) and sent to the client. When subsequent requests are made to the 
        //server, the token is validated, and the claims are extracted to identify the user and determine their permissions.
        //Claims are also used for authorization purposes.Once a user is authenticated, the claims associated with their identity 
        //    are checked to determine if they have access to specific resources or operations within the application.For example,
        //    roles claims might be used to restrict access to certain parts of the application based on the user's role.
    }


}
