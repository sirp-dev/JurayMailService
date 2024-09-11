using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;

namespace JurayMailService.Web.Areas.Admin.Pages.ManagerUser
{
    [Authorize(Roles = "Admin")]
    public class CreateUserModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<CreateUserModel> _logger;
                private readonly RoleManager<AppRole> _role;

        public CreateUserModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<CreateUserModel> logger,
            RoleManager<AppRole> role)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _role = role;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }


            [Display(Name = "Full Name")]
            public string Name { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = Input.Email, Email = Input.Email, PhoneNumber = Input.PhoneNumber, Fullname = Input.Name };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //AppRole Managerf = new AppRole
                    //{
                    //    Name = "Admin",
                    //    Description = "Description for the new role" // You can set this to any value you want
                    //};
                    //var checkManagerf = await _role.FindByNameAsync("Admin");

                    //if (checkManagerf == null)
                    //{
                    //    await _role.CreateAsync(Managerf);
                    //}

                    //var addrole = await _userManager.AddToRoleAsync(user, "Admin");



                    return RedirectToPage("./Index");
                     
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }

}
