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
    public sealed class UpdateEmailResponseStatusCommand : IRequest
    {
        public UpdateEmailResponseStatusCommand(EmailResponseStatus emailResponseStatus)
        {
            EmailResponseStatus = emailResponseStatus;
        }

        public EmailResponseStatus EmailResponseStatus { get; set; }


    }

    public class UpdateEmailResponseStatusCommandHandler : IRequestHandler<UpdateEmailResponseStatusCommand>
    {
        private readonly IEmailResponseStatusRepository _emailResponseStatusRepository;

        public UpdateEmailResponseStatusCommandHandler(IEmailResponseStatusRepository emailResponseStatusRepository)
        {
            _emailResponseStatusRepository = emailResponseStatusRepository;
        }

        public async Task Handle(UpdateEmailResponseStatusCommand request, CancellationToken cancellationToken)
        {

            await _emailResponseStatusRepository.UpdateAsync(request.EmailResponseStatus);
        }
    }
}
