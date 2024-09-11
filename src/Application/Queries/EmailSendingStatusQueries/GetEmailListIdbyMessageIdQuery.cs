using Application.DTO;
using Domain.DTO;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.EmailSendingStatusQueries
{
    public sealed class GetEmailListIdbyMessageIdQuery : IRequest<WebHookUpdateIds>
    {
        public GetEmailListIdbyMessageIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public class GetEmailListIdbyMessageIdQueryHandler : IRequestHandler<GetEmailListIdbyMessageIdQuery, WebHookUpdateIds>
        {
            private readonly IEmailSendingStatusRepository _emailSendingStatusRepository;

            public GetEmailListIdbyMessageIdQueryHandler(IEmailSendingStatusRepository emailSendingStatusRepository)
            {
                _emailSendingStatusRepository = emailSendingStatusRepository;
            }

            public async Task<WebHookUpdateIds> Handle(GetEmailListIdbyMessageIdQuery request, CancellationToken cancellationToken)
            {
                var outcome = await _emailSendingStatusRepository.GetEmailListIdByMessageId(request.Id);
                WebHookUpdateIds result = new WebHookUpdateIds
                {
                    EmailId = outcome.EmailId,
                    UserId = outcome.UserId,
                };
                return result;

            }
        }
    }
}
