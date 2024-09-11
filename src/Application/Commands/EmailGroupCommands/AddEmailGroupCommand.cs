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
    public sealed class AddEmailGroupCommand : IRequest
    {
        public AddEmailGroupCommand(EmailGroup emailGroup)
        {
            EmailGroup = emailGroup;
        }

        public EmailGroup EmailGroup { get; set; }


    }

    public class AddEmailGroupCommandHandler : IRequestHandler<AddEmailGroupCommand>
    {
        private readonly IEmailGroupRepository _emailGroupRepository;

        public AddEmailGroupCommandHandler(IEmailGroupRepository emailGroupRepository)
        {
            _emailGroupRepository = emailGroupRepository;
        }

        public async Task Handle(AddEmailGroupCommand request, CancellationToken cancellationToken)
        {

            await _emailGroupRepository.AddAsync(request.EmailGroup);


        }
    }
}
