using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EmailSendingStatusCommands
{
    public sealed class DeleteEmailSendingStatusCommand : IRequest
    {
        public DeleteEmailSendingStatusCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteEmailSendingStatusCommandHandler : IRequestHandler<DeleteEmailSendingStatusCommand>
    {
        private readonly IEmailSendingStatusRepository _emailSendingStatusRepository;

        public DeleteEmailSendingStatusCommandHandler(IEmailSendingStatusRepository emailSendingStatusRepository)
        {
            _emailSendingStatusRepository = emailSendingStatusRepository;
        }

        public async Task Handle(DeleteEmailSendingStatusCommand request, CancellationToken cancellationToken)
        {

            var emailSendingStatus = await _emailSendingStatusRepository.GetByIdAsync(request.Id);

            await _emailSendingStatusRepository.RemoveAsync(emailSendingStatus);

        }
    }
}
