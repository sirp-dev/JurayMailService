using Application.Commands.EmailGroupCommands;
using Application.Commands.EmailListCommands;
using Application.Queries.EmailGroupQueries;
using Application.Queries.EmailListQueries;
using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace JurayMailService.Web.Areas.User.Pages.Groups
{
    [Authorize]
    public class InfoModel : PageModel
    {
        private readonly IMediator _mediator;

        public InfoModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public EmailGroup EmailGroup { get; set; }
        public List<EmailList> EmailLists { get; set; }

        [BindProperty]
        public IFormFile ExcelFile { get; set; }

        [BindProperty]
        public long GroupId { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdEmailGroupQuery Command = new GetByIdEmailGroupQuery(id);
            EmailGroup = await _mediator.Send(Command);


            ListByGroupIdEmailListQuery listcommand = new ListByGroupIdEmailListQuery(id);
            EmailLists = await _mediator.Send(listcommand);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                UploadEmailsToGroup Command = new UploadEmailsToGroup(ExcelFile, GroupId, userId);
                await _mediator.Send(Command);
                TempData["success"] = "Success";
                return RedirectToPage("./Info", new {id = GroupId});
            }
            catch (Exception ex)
            {
                 
                GetByIdEmailGroupQuery Command = new GetByIdEmailGroupQuery(GroupId);
                EmailGroup = await _mediator.Send(Command);


                ListByGroupIdEmailListQuery listcommand = new ListByGroupIdEmailListQuery(GroupId);
                EmailLists = await _mediator.Send(listcommand);
                return Page();

            }
        }
    }

}
