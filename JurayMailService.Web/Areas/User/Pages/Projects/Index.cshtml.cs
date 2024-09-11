using Application.DTO;
using Application.Queries.DashboardQueries;
using Application.Queries.EmailProjectQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace JurayMailService.Web.Areas.User.Pages.Projects
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<EmailProject> EmailProjects { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ListProjectByUserId listQuery = new ListProjectByUserId(userId);
            EmailProjects = await _mediator.Send(listQuery);
            return Page();
        }
    }
}
