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
    public sealed class GetTotalCountEmailSendingStatusQuery : IRequest<int>
    {
        public string UserId { get; set; }

        public GetTotalCountEmailSendingStatusQuery(string userId)
        {
            UserId = userId;
        }

        public class GetTotalCountEmailSendingStatusQueryHandler : IRequestHandler<GetTotalCountEmailSendingStatusQuery, int>
        {
            private readonly IEmailSendingStatusRepository _emailSendingStatusRepository;

            public GetTotalCountEmailSendingStatusQueryHandler(IEmailSendingStatusRepository emailSendingStatusRepository)
            {
                _emailSendingStatusRepository = emailSendingStatusRepository;
            }

            public async Task<int> Handle(GetTotalCountEmailSendingStatusQuery request, CancellationToken cancellationToken)
            {
                return await _emailSendingStatusRepository.GetTotalCountByUserIdAsync(request.UserId);

            }
        }
    }
}
