using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EmailResponseStatusCommands
{
    public sealed class DeleteEmailResponseStatusCommand : IRequest
    {
        public DeleteEmailResponseStatusCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteEmailResponseStatusCommandHandler : IRequestHandler<DeleteEmailResponseStatusCommand>
    {
        private readonly IEmailResponseStatusRepository _emailResponseStatusRepository;

        public DeleteEmailResponseStatusCommandHandler(IEmailResponseStatusRepository emailResponseStatusRepository)
        {
            _emailResponseStatusRepository = emailResponseStatusRepository;
        }

        public async Task Handle(DeleteEmailResponseStatusCommand request, CancellationToken cancellationToken)
        {

            var emailResponseStatus = await _emailResponseStatusRepository.GetByIdAsync(request.Id);

            await _emailResponseStatusRepository.RemoveAsync(emailResponseStatus);

        }
    }
}
