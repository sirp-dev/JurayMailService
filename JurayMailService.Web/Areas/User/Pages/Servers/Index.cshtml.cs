using Application.Queries.EmailProjectQueries;
using Application.Queries.ServerQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace JurayMailService.Web.Areas.User.Pages.Servers
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<Server> Servers { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ListByUserIdServerQuery listQuery = new ListByUserIdServerQuery(userId);
            Servers = await _mediator.Send(listQuery);
            return Page();
        }
    }
}
