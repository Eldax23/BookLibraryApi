using Demo.Models.ViewModels;

namespace Demo.Services
{
    public interface IAuthService
    {

        Task<AuthModel> RegisterAsync(RegisterViewModel registerViewModel);
        Task<AuthModel> LoginAsync(LoginViewModel loginViewModel);

        Task<string> AddRoleAsync(AddRoleModel model);
    }
}
