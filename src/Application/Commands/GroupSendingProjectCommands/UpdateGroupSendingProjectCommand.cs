using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.GroupSendingProjectCommands
{
    public sealed class UpdateGroupSendingProjectCommand : IRequest
    {
        public UpdateGroupSendingProjectCommand(GroupSendingProject emailSendingStatus)
        {
            GroupSendingProject = emailSendingStatus;
        }

        public GroupSendingProject GroupSendingProject { get; set; }


    }

    public class UpdateGroupSendingProjectCommandHandler : IRequestHandler<UpdateGroupSendingProjectCommand>
    {
        private readonly IGroupSendingProjectRepository _emailSendingStatusRepository;

        public UpdateGroupSendingProjectCommandHandler(IGroupSendingProjectRepository emailSendingStatusRepository)
        {
            _emailSendingStatusRepository = emailSendingStatusRepository;
        }

        public async Task Handle(UpdateGroupSendingProjectCommand request, CancellationToken cancellationToken)
        {

            await _emailSendingStatusRepository.UpdateAsync(request.GroupSendingProject);
        }
    }
}
