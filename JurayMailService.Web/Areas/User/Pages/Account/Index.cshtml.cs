using Application.DTO;
using Application.Queries.DashboardQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace JurayMailService.Web.Areas.User.Pages.Account
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public DashboardDto DashboardDto { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            GetDashboardQuery dashQuery = new GetDashboardQuery(userId);
            DashboardDto = await _mediator.Send(dashQuery);
            return Page();
        }
    }
}
