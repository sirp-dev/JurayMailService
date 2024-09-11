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
    public sealed class GetByIdEmailSendingStatusQuery : IRequest<EmailSendingStatus>
    {
        public GetByIdEmailSendingStatusQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

        public class GetByIdEmailSendingStatusQueryHandler : IRequestHandler<GetByIdEmailSendingStatusQuery, EmailSendingStatus>
        {
            private readonly IEmailSendingStatusRepository _emailSendingStatusRepository;

            public GetByIdEmailSendingStatusQueryHandler(IEmailSendingStatusRepository emailSendingStatusRepository)
            {
                _emailSendingStatusRepository = emailSendingStatusRepository;
            }

            public async Task<EmailSendingStatus> Handle(GetByIdEmailSendingStatusQuery request, CancellationToken cancellationToken)
            {
                return await _emailSendingStatusRepository.GetEmailSendingById(request.Id);

            }
        }
    }

}
