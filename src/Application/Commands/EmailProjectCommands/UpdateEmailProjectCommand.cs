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
    public sealed class UpdateEmailProjectCommand : IRequest
    {
        public UpdateEmailProjectCommand(EmailProject emailProject)
        {
            EmailProject = emailProject;
        }

        public EmailProject EmailProject { get; set; }


    }

    public class UpdateEmailProjectCommandHandler : IRequestHandler<UpdateEmailProjectCommand>
    {
        private readonly IEmailProjectRepository _emailProjectRepository;

        public UpdateEmailProjectCommandHandler(IEmailProjectRepository emailProjectRepository)
        {
            _emailProjectRepository = emailProjectRepository;
        }

        public async Task Handle(UpdateEmailProjectCommand request, CancellationToken cancellationToken)
        {

            await _emailProjectRepository.UpdateAsync(request.EmailProject);
        }
    }
}
