using Application.Queries.EmailSendingStatusQueries;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EmailResponseStatusCommands
{
    public sealed class UpdateMailWebhookCommand : IRequest
    {
        public UpdateMailWebhookCommand(string? messageId, string? webhookLog, string? recordType, DateTime date)
        {
            MessageId = messageId;
            WebhookLog = webhookLog;
            RecordType = recordType;
            Date = date;
             
        }

        public string? MessageId { get; set; }
        public string? WebhookLog { get; set; }
        public string? RecordType { get; set; }
        public DateTime Date { get; set; }
 
    }

    public class UpdateMailWebhookCommandHandler : IRequestHandler<UpdateMailWebhookCommand>
    {
        private readonly IMediator _mediator;

        public UpdateMailWebhookCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(UpdateMailWebhookCommand request, CancellationToken cancellationToken)
        {
            //
            GetEmailListIdbyMessageIdQuery getQuery = new GetEmailListIdbyMessageIdQuery(request.MessageId);
            var outcome = await _mediator.Send(getQuery, cancellationToken);
            if (outcome != null)
            {
                //
                EmailResponseStatus newrecord = new EmailResponseStatus();
                newrecord.Log = request.WebhookLog;
                newrecord.RecordType = request.RecordType;
                newrecord.SentDate = request.Date;
                newrecord.MessageId = request.MessageId;
                newrecord.EmailListId = outcome.EmailId;
                newrecord.UserId  = outcome.UserId;
                AddEmailResponseStatusCommand command = new AddEmailResponseStatusCommand(newrecord);
                await _mediator.Send(command);
            }

        }
    }





}
