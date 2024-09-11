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
    public sealed class ListAllEmailSendingStatusQuery : IRequest<List<EmailSendingStatus>>
    {
        public class ListAllEmailSendingStatusQueryHandler : IRequestHandler<ListAllEmailSendingStatusQuery, List<EmailSendingStatus>>
        {
            private readonly IEmailSendingStatusRepository _emailResponseStatusRepository;

            public ListAllEmailSendingStatusQueryHandler(IEmailSendingStatusRepository emailResponseStatusRepository)
            {
                _emailResponseStatusRepository = emailResponseStatusRepository;
            }

            public async Task<List<EmailSendingStatus>> Handle(ListAllEmailSendingStatusQuery request, CancellationToken cancellationToken)
            {
                return await _emailResponseStatusRepository.GetAllAsync();

            }
        }
    }

}
