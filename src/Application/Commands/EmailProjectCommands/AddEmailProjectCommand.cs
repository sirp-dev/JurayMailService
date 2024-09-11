using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EmailProjectCommands
{
    public sealed class AddEmailProjectCommand : IRequest
    {
        public AddEmailProjectCommand(EmailProject emailProject)
        {
            EmailProject = emailProject;
        }

        public EmailProject EmailProject { get; set; }


    }

    public class AddEmailProjectCommandHandler : IRequestHandler<AddEmailProjectCommand>
    {
        private readonly IEmailProjectRepository _emailProjectRepository;

        public AddEmailProjectCommandHandler(IEmailProjectRepository emailProjectRepository)
        {
            _emailProjectRepository = emailProjectRepository;
        }

        public async Task Handle(AddEmailProjectCommand request, CancellationToken cancellationToken)
        {

            await _emailProjectRepository.AddAsync(request.EmailProject);


        }
    }
}
