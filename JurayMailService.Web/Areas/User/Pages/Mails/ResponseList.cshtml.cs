using Application.Queries.EmailResponseStatusQueries;
using Application.Queries.EmailSendingStatusQueries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace JurayMailService.Web.Areas.User.Pages.Mails
{
    public class ResponseListModel : PageModel
    {
        private readonly IMediator _mediator;

        public ResponseListModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 100;
        public string UserId { get; set; }
        //public int PageIndex { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public IEnumerable<EmailResponseStatus> EmailSendingStatus { get; set; }
        public async Task<IActionResult> OnGetAsync(int pagenumber)
        {
            if (pagenumber == 0)
            {
                pagenumber = 1;
            }
            PageNumber = pagenumber;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserId = userId;
            ListByQueryEmailResponseStatusQuery listQuery = new ListByQueryEmailResponseStatusQuery(userId, PageSize, PageNumber);
            EmailSendingStatus = await _mediator.Send(listQuery);



            GetTotalCountEmailResponseStatusQuery countCommand = new GetTotalCountEmailResponseStatusQuery(UserId);
            var totalCount = await _mediator.Send(countCommand);
            TotalPages = (int)Math.Ceiling((double)totalCount / PageSize);


            return Page();
        }
    }
}
