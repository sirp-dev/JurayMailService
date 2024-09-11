using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EmailSendingStatusCommands
{
    public sealed class UpdateEmailSendingStatusCommand : IRequest
    {
        public UpdateEmailSendingStatusCommand(EmailSendingStatus emailSendingStatus)
        {
            EmailSendingStatus = emailSendingStatus;
        }

        public EmailSendingStatus EmailSendingStatus { get; set; }


    }

    public class UpdateEmailSendingStatusCommandHandler : IRequestHandler<UpdateEmailSendingStatusCommand>
    {
        private readonly IEmailSendingStatusRepository _emailSendingStatusRepository;

        public UpdateEmailSendingStatusCommandHandler(IEmailSendingStatusRepository emailSendingStatusRepository)
        {
            _emailSendingStatusRepository = emailSendingStatusRepository;
        }

        public async Task Handle(UpdateEmailSendingStatusCommand request, CancellationToken cancellationToken)
        {

            await _emailSendingStatusRepository.UpdateAsync(request.EmailSendingStatus);
        }
    }
}
