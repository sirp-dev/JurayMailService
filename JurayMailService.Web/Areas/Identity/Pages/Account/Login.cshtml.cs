using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Domain.Models;

namespace JurayMailService.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<AppUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user != null)
            {

                var passcheck = await _userManager.CheckPasswordAsync(user, Input.Password);
 
                if (passcheck == true && user.TwoFactorEnabled == true)
                {
                    
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {

                        return LocalRedirect(returnUrl);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                        return Page();
                    }
                }
                else if (passcheck == true)
                {



                    await _signInManager.SignInAsync(user, isPersistent: false);
                     
 

                     var adminrole = await _userManager.IsInRoleAsync(user, "Admin"); 

                   
                    if (adminrole.Equals(true))
                    {
                        return RedirectToPage("/ManagerUser/Index", new { area = "Admin" });

                       
                    }
                    else
                    {
                        return RedirectToPage("/Account/Index", new { area = "User" });
                    }

                }

                else
                {


                    if (!string.IsNullOrEmpty(ErrorMessage))
                    {
                        ModelState.AddModelError(string.Empty, ErrorMessage);
                    }

                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                    return Page();
                }
            }
            else
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                return Page();
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
