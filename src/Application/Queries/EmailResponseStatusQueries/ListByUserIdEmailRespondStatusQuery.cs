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
   public sealed class ListByUserIdEmailRespondStatusQuery : IRequest<IEnumerable<EmailSendingStatus>>
    {
        public ListByUserIdEmailRespondStatusQuery(string userId, int pageSize, int pageNumber)
        {
            UserId = userId;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public string UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public class ListByUserIdEmailRespondStatusQueryHandler : IRequestHandler<ListByUserIdEmailRespondStatusQuery, IEnumerable<EmailSendingStatus>>
        {
            private readonly IEmailSendingStatusRepository _emailResponseStatusRepository;

            public ListByUserIdEmailRespondStatusQueryHandler(IEmailSendingStatusRepository emailResponseStatusRepository)
            {
                _emailResponseStatusRepository = emailResponseStatusRepository;
            }

            public async Task<IEnumerable<EmailSendingStatus>> Handle(ListByUserIdEmailRespondStatusQuery request, CancellationToken cancellationToken)
            {
                return await _emailResponseStatusRepository.GetListByUserIdAsync(request.PageNumber, request.PageSize, request.UserId);

            }
        }
    }
}
