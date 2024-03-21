using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Account;
using Football_Insight.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Controllers
{
    public class AccountController : BaseController
    {
        private readonly FootballInsightDbContext context;
        private readonly ILogger<HomeController> logger;
        private readonly IAccountService accountService;

        public AccountController(FootballInsightDbContext Context,
                                    ILogger<HomeController> Logger,
                                    IAccountService _accountService) 
        {
            context = Context;
            logger = Logger;
            accountService = _accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await accountService.GetUserViewModelAsync();

            return View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.RegisterUserAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
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
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.LoginAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt!");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await accountService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var viewModel = await accountService.GetUserEditFormAsync();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            var user = await accountService.GetUserAsync();
            
            if (accountService.GetUserAsync() == null)
            {
                return NotFound("User not found.");
            }

            if (ModelState.IsValid == false)
            {
                var viewModel = new UserEditViewModel
                {
                    Email = user.Email,
                    PhotoPath = accountService.GetUserPhotoPath(user),
                    Teams = await accountService.GetAllTeamsAsync(),
                    Players = await accountService.GetAllPlayersAsync()
                };

                return View(viewModel);
            }

            await accountService.EditAsync(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
