using Football_Insight.Data;
using Football_Insight.Data.Models;
using Football_Insight.Models;
using Football_Insight.Models.Account;
using Football_Insight.Models.Player;
using Football_Insight.Models.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net.Http;
using static Football_Insight.Data.DataConstants;

namespace Football_Insight.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly FootballInsightDbContext context;
        private readonly ILogger<HomeController> logger;

        public AccountController(FootballInsightDbContext Context,
                                ILogger<HomeController> Logger,
                                UserManager<ApplicationUser> UserManager,
                                SignInManager<ApplicationUser> SignInManager) 
        {
            userManager = UserManager;
            signInManager = SignInManager;
            context = Context;
            logger = Logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await GetUserAsync();
            var players = await GetPlayersAsync();
            var teams = await GetTeamsAsync();
            var userPhotoPath = await GetUserPhotoPath();
            var userFavoritePlayer = players?.Where(p => p.Id == user.FavoritePlayerId)?.Select(p => p.Name).FirstOrDefault()?.ToString();
            var userFavoriteTeam = teams?.Where(p => p.Id == user.FavoriteTeamId).Select(p => p.Name).FirstOrDefault()?.ToString();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var viewModel = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                City = user.City,
                Phone = user.PhoneNumber,
                Country = user.Country,
                FavoritePlayer = userFavoritePlayer,
                FavoriteTeam = userFavoriteTeam,
                PhotoPath = userPhotoPath
            };

            return View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email,  Email = model.Email };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt!");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await GetUserAsync();
            var players = await GetPlayersAsync();
            var teams = await GetTeamsAsync();
            var userPhotoPath = await GetUserPhotoPath();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var viewModel = new UserEditViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                Email = user.Email,
                PhotoPath = userPhotoPath,
                Teams = teams,
                Players = players,
                Country = user.Country,
                Phone = user.PhoneNumber,
                FavoritePlayerId = user.FavoritePlayerId,
                FavoriteTeamId = user.FavoriteTeamId
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            var user = await GetUserAsync();
            var players = await GetPlayersAsync();
            var teams = await GetTeamsAsync();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (!ModelState.IsValid)
            {
                var viewModel = new UserEditViewModel
                {
                    Email = user.Email,
                    PhotoPath = user.PhotoPath,
                    Teams = teams,
                    Players = players
                };

                return View(viewModel);
            }


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

                if (!string.IsNullOrEmpty(user.PhotoPath))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.PhotoPath);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }

                user.PhotoPath = $"/photos/{userFolder}/{newFileName}";
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.City = model.City;
            user.Country = model.Country;
            user.FavoritePlayerId = model.FavoritePlayerId;
            user.FavoriteTeamId = model.FavoriteTeamId;
            user.PhoneNumber = model.Phone; 

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<ApplicationUser> GetUserAsync() => await userManager.GetUserAsync(User);

        private async Task<string> GetUserPhotoPath()
        {
            var user = await GetUserAsync();

            if (!string.IsNullOrEmpty(user.PhotoPath))
            {
                return user.PhotoPath;
            }

            return DefaultPhotoPath;
        }

        private async Task<List<SimpleTeamViewModel>> GetTeamsAsync()
        {
            return await context.Teams
                .Select(t => new SimpleTeamViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        private async Task<List<PlayerDropdownViewModel>> GetPlayersAsync()
        {
            return await context.Players
                .Select(p => new PlayerDropdownViewModel
                {
                    Id = p.Id,
                    Name = $"{p.FirstName} {p.LastName}"
                })
                .ToListAsync();
        }
    }
}
