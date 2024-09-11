using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.PostMarkCommands
{
    public sealed class SendBatchEmailsCommand : IRequest
    {
        public SendBatchEmailsCommand(List<long> ids)
        {
            Ids = ids;
        }

        public List<long> Ids { get; set; }

    }

    public class SendBatchEmailsCommandHandler : IRequestHandler<SendBatchEmailsCommand>
    {
        private readonly IGroupSendingProjectRepository _emailSendingStatusRepository;

        public SendBatchEmailsCommandHandler(IGroupSendingProjectRepository emailSendingStatusRepository)
        {
            _emailSendingStatusRepository = emailSendingStatusRepository;
        }

        public async Task Handle(SendBatchEmailsCommand request, CancellationToken cancellationToken)
        {

         //   await _emailSendingStatusRepository.AddAsync(request.GroupSendingProject);


        }
    }
}
