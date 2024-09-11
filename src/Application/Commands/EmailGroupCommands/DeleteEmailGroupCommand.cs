using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EmailGroupCommands
{
      public sealed class DeleteEmailGroupCommand : IRequest
    {
        public DeleteEmailGroupCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteEmailGroupCommandHandler : IRequestHandler<DeleteEmailGroupCommand>
    {
        private readonly IEmailGroupRepository _emailGroupRepository;

        public DeleteEmailGroupCommandHandler(IEmailGroupRepository emailGroupRepository)
        {
            _emailGroupRepository = emailGroupRepository;
        }

        public async Task Handle(DeleteEmailGroupCommand request, CancellationToken cancellationToken)
        {

            var emailGroup = await _emailGroupRepository.GetByIdAsync(request.Id);

            await _emailGroupRepository.RemoveAsync(emailGroup);

        }
    }
}
