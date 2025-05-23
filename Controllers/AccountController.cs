using System.Text.Json;
using InsuranceWebApp.Data;
using InsuranceWebApp.Models;
using InsuranceWebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private ILogger<AccountController> _logger;
        private readonly IZaloApiService _zaloApiService;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AccountController> logger,
            IZaloApiService zaloApiService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _zaloApiService = zaloApiService;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                {
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(userName: model.Email,
                    password: model.Password,
                    isPersistent: model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Hospital");
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Password), "Incorrect Password");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Invalid model");
                return View(model);
            }
            var existsEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existsEmail != null)
            {
                ModelState.AddModelError(nameof(model.Email), "Email already exist");
                return View(model);
            }
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(nameof(model.ConfirmPassword), "Confirm password does match with password above");
                return View(model);
            }
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                _logger.LogInformation("Registration failed !!");
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View(model);
            }
            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("Login", "Account");
        }
        //[HttpPost]
        //public async Task<IActionResult> ForgotPassord(ForgotPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = await _userManager.FindByEmailAsync(model.Email);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError(nameof(model.Email), "Tài khoản không tồn tại");
        //        return View(model);
        //    }
        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
        //}
        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError(nameof(model.Email), "Something is wrong! please Enter your email again");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ResetPassword", "Account", new { username = user.UserName });
                }
            }
            _logger.LogInformation("Invalid Model");
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Hospital");
        }
        
        
        
        public IActionResult ChangePassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }
            return View(new ChangePasswordViewModel { Email = email });
        }

        public async Task<IActionResult> GetZaloUserLocation(string userAccessToken,string token)
        {
            var result= await _zaloApiService.GetZaloUserInfoAsync(userAccessToken,token);
            var jsonString = JsonDocument.Parse(result);
            return Json(jsonString);
        }
     

    }


}
