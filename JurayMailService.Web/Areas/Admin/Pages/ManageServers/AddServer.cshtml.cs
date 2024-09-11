using Application.Commands.EmailProjectCommands;
using Application.Commands.ServerCommands;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace JurayMailService.Web.Areas.Admin.Pages.ManageServers
{

    [Authorize]
    public class AddServerModel : PageModel
    {
        private readonly IMediator _mediator;

        public AddServerModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public Server Server { get; set; }


        [BindProperty]
        public string UserId { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            UserId = id;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            { 
                AddServerCommand Command = new AddServerCommand(Server);
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
