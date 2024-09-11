using Application.DTO;
using Application.Queries.EmailGroupQueries;
using Application.Queries.EmailProjectQueries;
using Application.Queries.EmailResponseStatusQueries;
using Application.Queries.EmailSendingStatusQueries;
using Application.Queries.GroupSendingProjectQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JurayMailService.Web.Areas.User.Pages.Mails
{
    public class InfoModel : PageModel
    {
        private readonly IMediator _mediator;

        public InfoModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public MailInfoDto MailInfoDto { get; set; }

         
        public async Task<IActionResult> OnGetAsync(long id, string mxs)
        {
            if (id < 0)
            {
                return NotFound();
            }
            ListAllEmailResponseByMessageIdQuery Command = new ListAllEmailResponseByMessageIdQuery(mxs, id);
            MailInfoDto = await _mediator.Send(Command);

             
            return Page();
        }

    }
}
