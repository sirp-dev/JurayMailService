using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.GroupSendingProjectCommands
{
      public sealed class DeleteGroupSendingProjectCommand : IRequest
    {
        public DeleteGroupSendingProjectCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteGroupSendingProjectCommandHandler : IRequestHandler<DeleteGroupSendingProjectCommand>
    {
        private readonly IGroupSendingProjectRepository _emailSendingStatusRepository;

        public DeleteGroupSendingProjectCommandHandler(IGroupSendingProjectRepository emailSendingStatusRepository)
        {
            _emailSendingStatusRepository = emailSendingStatusRepository;
        }

        public async Task Handle(DeleteGroupSendingProjectCommand request, CancellationToken cancellationToken)
        {

            var emailSendingStatus = await _emailSendingStatusRepository.GetByIdAsync(request.Id);

            await _emailSendingStatusRepository.RemoveAsync(emailSendingStatus);

        }
    }
}
