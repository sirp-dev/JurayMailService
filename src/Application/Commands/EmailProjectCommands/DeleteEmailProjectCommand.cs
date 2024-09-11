using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EmailProjectCommands
{
    public sealed class DeleteEmailProjectCommand : IRequest
    {
        public DeleteEmailProjectCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteEmailProjectCommandHandler : IRequestHandler<DeleteEmailProjectCommand>
    {
        private readonly IEmailProjectRepository _emailProjectRepository;

        public DeleteEmailProjectCommandHandler(IEmailProjectRepository emailProjectRepository)
        {
            _emailProjectRepository = emailProjectRepository;
        }

        public async Task Handle(DeleteEmailProjectCommand request, CancellationToken cancellationToken)
        {

            var emailProject = await _emailProjectRepository.GetByIdAsync(request.Id);

            await _emailProjectRepository.RemoveAsync(emailProject);

        }
    }
}
