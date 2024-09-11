using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JurayMailService.Web.Areas.Admin.Pages.ManagerUser
{
    [Authorize(Roles = "Admin")]

    public class IndexModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<IndexModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IList<AppUser> Users { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Users = await _userManager.Users.Where(x=>x.Email != "admin@juray.ng")
                .ToListAsync();
            
            return Page();
        }
    }
}
