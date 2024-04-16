using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Account;
using Football_Insight.Core.Models.Match;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Football_Insight.Core.Constants.GlobalConstants;

namespace Football_Insight.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository repo;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ITeamService teamService;
        private readonly IPlayerService playerService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountService(IRepository _repo, 
                              ITeamService _teamService,
                              IPlayerService _playerService,
                              IHttpContextAccessor _httpContextAccessor,
                              UserManager<ApplicationUser> _userManager, 
                              SignInManager<ApplicationUser> _signInManager)
        {
            repo = _repo;
            userManager = _userManager;
            teamService = _teamService;
            httpContextAccessor = _httpContextAccessor;
            playerService = _playerService;
            signInManager = _signInManager;
        }

        public async Task<OperationResult> EditAsync(UserEditViewModel model)
        {
            try
            {
                var user = await GetUserAsync();
                if (user == null) return new OperationResult(false, "User not found.");

                if (model.Photo != null && model.Photo.Length > 0)
                {
                    string photoResult = await SaveUserProfilePhoto(model.Photo, user.Id);
                    if (photoResult != "Success")
                    {
                        return new OperationResult(false, photoResult);
                    }
                }

                UpdateUserProperties(user, model);

                await repo.SaveChangesAsync();
                return new OperationResult(true, "Profile updated successfully.");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"An error occurred: {ex.Message}");
            }
        }

        private async Task<string> SaveUserProfilePhoto(IFormFile photo, string userId)
        {
            var userFolder = userId.ToString();
            var uploadsRootFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos/");
            var userFolderPath = Path.Combine(uploadsRootFolder, userFolder);

            if (!Directory.Exists(userFolderPath))
            {
                Directory.CreateDirectory(userFolderPath);
            }

            var newFileName = "profile_picture.jpg";
            var savePath = Path.Combine(userFolderPath, newFileName);

            try
            {
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                var user = await GetUserAsync();
                user.PhotoPath = $"/photos/{userFolder}/{newFileName}";
                await repo.SaveChangesAsync();

                return "Success";
            }
            catch (Exception ex)
            {
                return $"Error saving photo: {ex.Message}";
            }
        }

        private void UpdateUserProperties(ApplicationUser user, UserEditViewModel model)
        {
            user.Country = model.Country != "None" ? model.Country : null;
            user.FavoritePlayerId = model.FavoritePlayerId != -1 ? model.FavoritePlayerId : null;
            user.FavoriteTeamId = model.FavoriteTeamId != -1 ? model.FavoriteTeamId : null;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.City = model.City;
            user.PhoneNumber = model.Phone;
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
                Players = await playerService.GetAllPlayersAsync(),
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
            return !string.IsNullOrEmpty(user.PhotoPath) ? user.PhotoPath : Constants.GlobalConstants.DefaultPhotoPath;
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
                await userManager.AddToRoleAsync(user, "User");
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

        public async Task<List<MatchFavoriteViewModel>> GetFavoriteMatchesAsync()
        {
            var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var matches = await repo.AllReadonly<Favorite>()
                .Where(f => f.UserId == userId)
                .Select(f => new MatchFavoriteViewModel
                {
                    MatchId = f.Match.Id,
                    HomeTeam = f.Match.HomeTeam.Name,
                    AwayTeam = f.Match.AwayTeam.Name,
                    MatchStatus = f.Match.Status,
                    LeagueName = f.Match.League.Name,
                    DateAndTime = f.Match.DateAndTime.ToString(DateAndTimeFormat),
                    LeagueId = f.Match.LeagueId,
                    HomeGoals = f.Match.HomeScore,
                    AwayGoals = f.Match.AwayScore
                })
                .ToListAsync();

            return matches;
        }
    }
}
