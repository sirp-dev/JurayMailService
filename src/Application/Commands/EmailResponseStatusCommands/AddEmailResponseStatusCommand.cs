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
    public sealed class AddEmailResponseStatusCommand : IRequest
    {
        public AddEmailResponseStatusCommand(EmailResponseStatus emailResponseStatus)
        {
            EmailResponseStatus = emailResponseStatus;
        }

        public EmailResponseStatus EmailResponseStatus { get; set; }


    }

    public class AddEmailResponseStatusCommandHandler : IRequestHandler<AddEmailResponseStatusCommand>
    {
        private readonly IEmailResponseStatusRepository _emailResponseStatusRepository;

        public AddEmailResponseStatusCommandHandler(IEmailResponseStatusRepository emailResponseStatusRepository)
        {
            _emailResponseStatusRepository = emailResponseStatusRepository;
        }

        public async Task Handle(AddEmailResponseStatusCommand request, CancellationToken cancellationToken)
        {

            await _emailResponseStatusRepository.AddAsync(request.EmailResponseStatus);


        }
    }
}
