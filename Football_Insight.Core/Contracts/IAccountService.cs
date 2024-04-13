using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Account;
using Football_Insight.Core.Models.Player;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Football_Insight.Core.Contracts
{
    public interface IAccountService
    {
        Task<RegistrationResultViewModel> RegisterUserAsync(UserRegisterViewModel model);

        Task<SignInResult> LoginAsync(UserLoginViewModel model);

        Task LogoutAsync();

        Task<OperationResult> EditAsync(UserEditViewModel model);

        Task<ApplicationUser> GetUserAsync();

        Task<UserEditViewModel> GetUserEditFormAsync();

        Task<UserViewModel> GetUserViewModelAsync();

        string GetUserPhotoPath(ApplicationUser user);
    }
}
