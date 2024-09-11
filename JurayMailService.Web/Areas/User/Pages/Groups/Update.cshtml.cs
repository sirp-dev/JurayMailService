using Application.Commands.EmailGroupCommands;
using Application.Queries.EmailGroupQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JurayMailService.Web.Areas.User.Pages.Groups
{
    [Authorize]
    public class UpdateModel : PageModel
    {
        private readonly IMediator _mediator;

        public UpdateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public EmailGroup EmailGroup { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdEmailGroupQuery Command = new GetByIdEmailGroupQuery(id);
            EmailGroup = await _mediator.Send(Command);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                UpdateEmailGroupCommand Command = new UpdateEmailGroupCommand(EmailGroup);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                return Page();

            }
        }
    }

}
