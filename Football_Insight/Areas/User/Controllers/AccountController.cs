using Football_Insight.Controllers;
using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class AccountController : BaseController
    {

        private readonly ILogger<HomeController> logger;
        private readonly ITeamService teamService;
        private readonly IAccountService accountService;
        private readonly IPlayerService playerService;

        public AccountController(ILogger<HomeController> Logger,
                                 ITeamService _teamService,
                                 IAccountService _accountService,
                                 IPlayerService _playerService)
        {

            logger = Logger;
            teamService = _teamService;
            accountService = _accountService;
            playerService = _playerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var viewModel = await accountService.GetUserViewModelAsync();

                if (viewModel == null)
                {
                    return NotFound();
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retriving user details view.");
                return StatusCode(500);
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            try
            {
                if (User.Identity != null && User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home", new { Area = "User" });
                }

                return View();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retriving user registration form.");
                return StatusCode(500);
            }
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await accountService.RegisterUserAsync(model);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home", new { Area = "User" });
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error during user registration for {Username}", model.Email);
                    ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            try
            {
                if (User.Identity != null && User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home", new { Area = "User" });
                }

                return View();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retriving user login form.");
                return StatusCode(500);
            }
        }

        
        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await accountService.LoginAsync(model);
                    if (!result.Succeeded)
                    {
                        logger.LogWarning("Invalid login attempt for {Username}", model.Email);
                        ModelState.AddModelError(string.Empty, "Invalid login attempt!");
                        return View(model);
                    }

                    return RedirectToAction("Index", "Home", new { Area = "User" });
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error during user login for {Username}", model.Email);
                    ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                }
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await accountService.LogoutAsync();

                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            try
            {
                var viewModel = await accountService.GetUserEditFormAsync();
                if (viewModel == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving user edit form data.");
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            try
            {
                var user = await accountService.GetUserAsync();
                if (user == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    var viewModel = new UserEditViewModel
                    {
                        Email = user.Email,
                        PhotoPath = accountService.GetUserPhotoPath(user),
                        Teams = await teamService.GetAllTeamsAsync(),
                        Players = await playerService.GetAllPlayersAsync()
                    };

                    return View(viewModel);
                }

                var result = await accountService.EditAsync(model);
                if (!result.Success)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while editing user profile.");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult AccessDenied(string returnUrl = null)
        {
            return View();
        }
    }
}
