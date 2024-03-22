using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Account;
using Football_Insight.Core.Models.Player;
using Football_Insight.Core.Models.Team;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository repo;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ITeamService teamService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountService(IRepository _repo, 
                              ITeamService _teamService,
                              IHttpContextAccessor _httpContextAccessor,
                              UserManager<ApplicationUser> _userManager, 
                              SignInManager<ApplicationUser> _signInManager)
        {
            repo = _repo;
            userManager = _userManager;
            teamService = _teamService;
            httpContextAccessor = _httpContextAccessor;
            signInManager = _signInManager;
        }

        public Task Edit()
        {
            throw new NotImplementedException();
        }

        public async Task EditAsync(UserEditViewModel model)
        {
            var user = await GetUserAsync();

            if (model.Photo != null && model.Photo.Length > 0)
            {
                var userFolder = user.Id.ToString();
                var uploadsRootFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos/");
                var userFolderPath = Path.Combine(uploadsRootFolder, userFolder);

                if (!Directory.Exists(userFolderPath))
                {
                    Directory.CreateDirectory(userFolderPath);
                }

                var newFileName = "profile_picture.jpg";

                var savePath = Path.Combine(userFolderPath, newFileName);

                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }

                user.PhotoPath = $"/photos/{userFolder}/{newFileName}";
            }

            if (model != null)
            {
                user.Country = model.Country != "None" ? model.Country : null;
                user.FavoritePlayerId = model.FavoritePlayerId != -1 ? model.FavoritePlayerId : null;
                user.FavoriteTeamId = model.FavoriteTeamId != -1 ? model.FavoriteTeamId : null;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.City = model.City;
                user.PhoneNumber = model.Phone;

                await repo.SaveChangesAsync();
            }
        }

        public async Task<UserEditViewModel> GetUserEditFormAsync()
        {
            var user = await GetUserAsync();

            var viewModel = new UserEditViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                Email = user.Email,
                PhotoPath = GetUserPhotoPath(user),
                Teams = await teamService.GetAllTeamsAsync(),
                Players = await GetAllPlayersAsync(),
                Country = user.Country,
                Phone = user.PhoneNumber,
                FavoritePlayerId = user.FavoritePlayerId,
                FavoriteTeamId = user.FavoriteTeamId
            };

            return viewModel;
        }

        public async Task<UserViewModel> GetUserViewModelAsync()
        {
            var user = await GetUserAsync();

            var viewModel = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                City = user.City,
                Phone = user.PhoneNumber,
                Country = user.Country,
                FavoritePlayer = await GetFavoritePlayerNameAsync(user),
                FavoriteTeam = await GetFavoriteTeamNameAsync(user),
                PhotoPath = GetUserPhotoPath(user)
        };

            return viewModel;
        }

        public async Task<ApplicationUser> GetUserAsync()
        {
            return await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);
        }

        public string GetUserPhotoPath(ApplicationUser user)
        {
            return !string.IsNullOrEmpty(user.PhotoPath) ? user.PhotoPath : Constants.MessageConstants.DefaultPhotoPath;
        }

        public async Task<List<PlayerSimpleViewModel>> GetAllPlayersAsync()
        {
            return await repo.All<Player>()
                .Select(p => new PlayerSimpleViewModel
                {
                    Id = p.Id,
                    Name = $"{p.FirstName} {p.LastName}"
                })
                .ToListAsync();
        }



        private async Task<string> GetFavoritePlayerNameAsync(ApplicationUser user)
        {
            if(user.FavoritePlayerId == null)
            {
                return string.Empty;
            }

            var player = await repo.GetByIdAsync<Player>(user.FavoritePlayerId);

            return $"{player.FirstName} {player.LastName}";
        }

        private async Task<string> GetFavoriteTeamNameAsync(ApplicationUser user)
        {
            if (user.FavoriteTeamId == null)
            {
                return string.Empty;
            }

            var team = await repo.GetByIdAsync<Team>(user.FavoriteTeamId);

            return team.Name;
        }

        public async Task<RegistrationResultViewModel> RegisterUserAsync(UserRegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return new RegistrationResultViewModel { Succeeded = true, User = user };
            }

            return new RegistrationResultViewModel { Succeeded = false, Errors = result.Errors.Select(e => e.Description) };
        }

        public async Task<SignInResult> LoginAsync(UserLoginViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
