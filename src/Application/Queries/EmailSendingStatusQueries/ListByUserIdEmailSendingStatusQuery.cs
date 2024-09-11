using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EmailSendingStatusQueries
{
    public sealed class ListByUserIdEmailSendingStatusQuery : IRequest<IEnumerable<EmailSendingStatus>>
    {
        public ListByUserIdEmailSendingStatusQuery(string userId, int pageSize, int pageNumber)
        {
            UserId = userId;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public string UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public class ListByUserIdEmailSendingStatusQueryHandler : IRequestHandler<ListByUserIdEmailSendingStatusQuery, IEnumerable<EmailSendingStatus>>
        {
            private readonly IEmailSendingStatusRepository _emailResponseStatusRepository;

            public ListByUserIdEmailSendingStatusQueryHandler(IEmailSendingStatusRepository emailResponseStatusRepository)
            {
                _emailResponseStatusRepository = emailResponseStatusRepository;
            }

            public async Task<IEnumerable<EmailSendingStatus>> Handle(ListByUserIdEmailSendingStatusQuery request, CancellationToken cancellationToken)
            {
                return await _emailResponseStatusRepository.GetListByUserIdAsync(request.PageNumber, request.PageSize, request.UserId);

            }
        }
    }
}
