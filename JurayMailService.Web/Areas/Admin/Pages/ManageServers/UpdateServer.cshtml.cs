using Application.Commands.EmailProjectCommands;
using Application.Commands.ServerCommands;
using Application.Queries.EmailProjectQueries;
using Application.Queries.ServerQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JurayMailService.Web.Areas.Admin.Pages.ManageServers
{
     [Authorize]
    public class UpdateServerModel : PageModel
    {
        private readonly IMediator _mediator;

        public UpdateServerModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public Server Server { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdServerQuery Command = new GetByIdServerQuery(id);
            Server = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                UpdateServerCommand Command = new UpdateServerCommand(Server);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./ServerByUser", new {id = Server.UserId});
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }
}
