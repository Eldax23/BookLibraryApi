using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Demo.Helpers;
using Demo.Models.DB;
using Demo.Models.DB.Entites;
using Demo.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Demo.Services
{
    public class AuthService : IAuthService
    {
        private readonly JWT jwt;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext context;

        public AuthService( UserManager<ApplicationUser> _userManager , RoleManager<IdentityRole> _roleManager ,  IOptions<JWT> _jwt , AppDbContext _context)
        {
            jwt = _jwt.Value;
            userManager = _userManager;
            roleManager = _roleManager;
            context = _context;
        }
        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(model.UserId);
            if (user is null)
                return "UserId Doesn't exist.";

            if (!await roleManager.RoleExistsAsync(model.RoleName))
                return "There is no Role With that Name.";

            if (await userManager.IsInRoleAsync(user, model.RoleName))
                return "User Already Assigned To This role";

            IdentityRole role = new IdentityRole(model.RoleName);
            IdentityResult result = await userManager.AddToRoleAsync(user , model.RoleName);

            if (result.Succeeded)
            {
                return string.Empty;
            }
            else
            {
                string errors = string.Empty;
                foreach (IdentityError error in result.Errors)
                {
                    errors += error.Description + " ,";
                }
                return errors;
            }

        }

        public async Task<AuthModel> LoginAsync(LoginViewModel loginViewModel)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(loginViewModel.Email);
            if(user == null)
            {
                return new AuthModel() { Message = "Invalid Email" };
            }

            bool validPassword = await userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if(!validPassword)
            {
                return new AuthModel() { Message = "Password is Invalid" };
            }

            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);

            IList<string>roles = await userManager.GetRolesAsync(user);
            return new AuthModel()
            {
                Message = "Login Successfull.",
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = loginViewModel.Email,
                Roles = roles.ToList(),
                ExpiresOn = jwtSecurityToken.ValidTo,
            };


           
        }

        public async Task<AuthModel> RegisterAsync(RegisterViewModel registerViewModel)
        {
            //This is just for enhanced testing
            //Its better if you check both and Use "UserName Or Email Already Exists" For Security Reasons
            //Check if UserName Exists
            Customer customer = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Address = registerViewModel.Address,
                DateOfBirth = registerViewModel.DateOfBirth,
            };

            context.Customers.Add(customer);

            if (await userManager.FindByNameAsync(registerViewModel.UserName) is not null)
            {
                return new AuthModel() { Message = "UserName Already Exists" };
            }

            //Check if Email Exists
            if(await userManager.FindByEmailAsync(registerViewModel.Email) is not null)
            {
                return new AuthModel()
                {
                    Message = "Email Already Exists"
                };
            }

            ApplicationUser user = new ApplicationUser()
            {
                
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                CustomerID = customer.Id,
            };


            IdentityResult result = await userManager.CreateAsync(user , registerViewModel.Password);
            if (!result.Succeeded)
            {
                string errors = "";
                foreach(IdentityError error in result.Errors)
                {
                    errors += error.Description;
                }
                return new AuthModel() { Message = errors};
            }

            await userManager.AddToRoleAsync(user, "User");

            JwtSecurityToken securityToken = await CreateJwtToken(user);
            return new AuthModel()
            {
                Message = "Register Successfull.",
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Email = registerViewModel.Email,
                Roles = new List<string> { "User" },
                ExpiresOn = securityToken.ValidTo,
            };
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            IList<Claim> userClaims = await userManager.GetClaimsAsync(user);
            IList<string> roles = await userManager.GetRolesAsync(user);

            List<Claim> roleClaims = new List<Claim>();
            foreach(var role in roles)
            {
                roleClaims.Add(new Claim("roles" , role));
            }


            var allClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub , user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email , user.Email),
                new Claim("cid" , user.CustomerID.ToString()),
                new Claim("uid" , user.Id),
            }.Union(roleClaims).Union(userClaims);


            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: allClaims,
                expires: DateTime.Now.AddDays(jwt.DurationInDays),
                signingCredentials: signingCredentials
                
                );

            return securityToken;

            
        }
    }
}
