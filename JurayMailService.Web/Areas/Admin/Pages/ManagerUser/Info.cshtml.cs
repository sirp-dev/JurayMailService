using Application.Queries.EmailProjectQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace JurayMailService.Web.Areas.Admin.Pages.ManagerUser
{
    [Authorize]
    public class InfoModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;

        public InfoModel(IMediator mediator, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [BindProperty]
        public AppUser AppUser { get; set; }


        [BindProperty]
        public EmailSendingStatus EmailSendingStatus { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
           AppUser = await _userManager.FindByIdAsync(id);
            return Page();
        }

     }

}
