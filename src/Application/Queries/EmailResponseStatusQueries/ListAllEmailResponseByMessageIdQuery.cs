using Application.DTO;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EmailResponseStatusQueries
{
    public sealed class ListAllEmailResponseByMessageIdQuery : IRequest<MailInfoDto>
    {
        public ListAllEmailResponseByMessageIdQuery(string messageId, long id)
        {
            MessageId = messageId;
            Id = id;
        }

        public string MessageId { get; set; }
        public long Id { get; set; }


        public class ListAllEmailResponseByMessageIdQueryHandler : IRequestHandler<ListAllEmailResponseByMessageIdQuery, MailInfoDto>
        {
            private readonly IEmailResponseStatusRepository _emailResponseStatusRepository;
            private readonly IEmailSendingStatusRepository _emailSendingStatusRepository;

            public ListAllEmailResponseByMessageIdQueryHandler(IEmailResponseStatusRepository emailResponseStatusRepository, IEmailSendingStatusRepository emailSendingStatusRepository)
            {
                _emailResponseStatusRepository = emailResponseStatusRepository;
                _emailSendingStatusRepository = emailSendingStatusRepository;
            }

            public async Task<MailInfoDto> Handle(ListAllEmailResponseByMessageIdQuery request, CancellationToken cancellationToken)
            {
                MailInfoDto rex = new MailInfoDto();
                rex.EmailResponseStatusList = await _emailResponseStatusRepository.GetAllResponseByMessageId(request.MessageId);

                //
                rex.EmailSendingStatus = await _emailSendingStatusRepository.GetEmailSendingById(request.Id);
                return rex;
            }
        }
    }
}
