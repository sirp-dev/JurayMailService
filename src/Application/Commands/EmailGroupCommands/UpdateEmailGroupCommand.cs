using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EmailGroupCommands
{
    public sealed class UpdateEmailGroupCommand : IRequest
    {
        public UpdateEmailGroupCommand(EmailGroup emailGroup)
        {
            EmailGroup = emailGroup;
        }

        public EmailGroup EmailGroup { get; set; }


    }

    public class UpdateEmailGroupCommandHandler : IRequestHandler<UpdateEmailGroupCommand>
    {
        private readonly IEmailGroupRepository _emailGroupRepository;

        public UpdateEmailGroupCommandHandler(IEmailGroupRepository emailGroupRepository)
        {
            _emailGroupRepository = emailGroupRepository;
        }

        public async Task Handle(UpdateEmailGroupCommand request, CancellationToken cancellationToken)
        {

            await _emailGroupRepository.UpdateAsync(request.EmailGroup);
        }
    }
}
