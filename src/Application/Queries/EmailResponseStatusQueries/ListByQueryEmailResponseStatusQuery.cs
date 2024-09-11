using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EmailResponseStatusQueries
{
    public sealed class ListByQueryEmailResponseStatusQuery : IRequest<IEnumerable<EmailResponseStatus>>
    {
        public ListByQueryEmailResponseStatusQuery(string userId, int pageSize, int pageNumber)
        {
            UserId = userId;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public string UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public class ListByQueryEmailResponseStatusQueryHandler : IRequestHandler<ListByQueryEmailResponseStatusQuery, IEnumerable<EmailResponseStatus>>
        {
            private readonly IEmailSendingStatusRepository _emailResponseStatusRepository;

            public ListByQueryEmailResponseStatusQueryHandler(IEmailSendingStatusRepository emailResponseStatusRepository)
            {
                _emailResponseStatusRepository = emailResponseStatusRepository;
            }

            public async Task<IEnumerable<EmailResponseStatus>> Handle(ListByQueryEmailResponseStatusQuery request, CancellationToken cancellationToken)
            {
                return await _emailResponseStatusRepository.GetResponseListByUserIdAsync(request.PageNumber, request.PageSize, request.UserId);

            }
        }
    }
}
