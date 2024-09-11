using Application.Queries.ServerQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JurayMailService.Web.Areas.Admin.Pages.ManageServers
{
    [Authorize]
    public class ListServersModel : PageModel
    {
        private readonly IMediator _mediator;

        public ListServersModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<Server> Servers { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            ListAllServerQuery listQuery = new ListAllServerQuery();
            Servers = await _mediator.Send(listQuery);
            return Page();
        }
    }
}
