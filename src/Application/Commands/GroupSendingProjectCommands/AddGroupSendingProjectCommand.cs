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
    public sealed class AddGroupSendingProjectCommand : IRequest
    {
        public AddGroupSendingProjectCommand(GroupSendingProject emailSendingStatus)
        {
            GroupSendingProject = emailSendingStatus;
        }

        public GroupSendingProject GroupSendingProject { get; set; }


    }

    public class AddGroupSendingProjectCommandHandler : IRequestHandler<AddGroupSendingProjectCommand>
    {
        private readonly IGroupSendingProjectRepository _emailSendingStatusRepository;

        public AddGroupSendingProjectCommandHandler(IGroupSendingProjectRepository emailSendingStatusRepository)
        {
            _emailSendingStatusRepository = emailSendingStatusRepository;
        }

        public async Task Handle(AddGroupSendingProjectCommand request, CancellationToken cancellationToken)
        {

            await _emailSendingStatusRepository.AddAsync(request.GroupSendingProject);


        }
    }
}
