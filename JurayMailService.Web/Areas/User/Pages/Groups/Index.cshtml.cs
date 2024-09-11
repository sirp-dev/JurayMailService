using Application.Queries.EmailGroupQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace JurayMailService.Web.Areas.User.Pages.Groups
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<EmailGroup> EmailGroups { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ListAllByUserIdEmailGroupQuery listQuery = new ListAllByUserIdEmailGroupQuery(userId);
            EmailGroups = await _mediator.Send(listQuery);
            return Page();
        }
    }
}
