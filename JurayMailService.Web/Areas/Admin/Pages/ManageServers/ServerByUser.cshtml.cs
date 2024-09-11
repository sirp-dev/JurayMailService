using Application.Queries.ServerQueries;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace JurayMailService.Web.Areas.Admin.Pages.ManageServers
{
    [Authorize]
    public class ServerByUserModel : PageModel
    {
        private readonly IMediator _mediator;

        public ServerByUserModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<Server> Servers { get; set; }

        public string? UserId { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            UserId = id;
            ListByUserIdServerQuery listQuery = new ListByUserIdServerQuery(id);
            Servers = await _mediator.Send(listQuery);
            return Page();
        }
    }
}
