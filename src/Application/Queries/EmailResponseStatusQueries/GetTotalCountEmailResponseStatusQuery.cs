using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EmailResponseStatusQueries
{
    public sealed class GetTotalCountEmailResponseStatusQuery : IRequest<int>
    {
        public string UserId { get; set; }

        public GetTotalCountEmailResponseStatusQuery(string userId)
        {
            UserId = userId;
        }

        public class GetTotalCountEmailResponseStatusQueryHandler : IRequestHandler<GetTotalCountEmailResponseStatusQuery, int>
        {
            private readonly IEmailSendingStatusRepository _emailResponseStatusRepository;

            public GetTotalCountEmailResponseStatusQueryHandler(IEmailSendingStatusRepository emailResponseStatusRepository)
            {
                _emailResponseStatusRepository = emailResponseStatusRepository;
            }

            public async Task<int> Handle(GetTotalCountEmailResponseStatusQuery request, CancellationToken cancellationToken)
            {
                return await _emailResponseStatusRepository.GetResponseTotalCountByUserIdAsync(request.UserId);

            }
        }
    }
}
