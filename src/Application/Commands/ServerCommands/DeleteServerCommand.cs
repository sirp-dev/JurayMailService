using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ServerCommands
{
    public sealed class DeleteServerCommand : IRequest
    {
        public DeleteServerCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteServerCommandHandler : IRequestHandler<DeleteServerCommand>
    {
        private readonly IServerRepository _serverRepository;

        public DeleteServerCommandHandler(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task Handle(DeleteServerCommand request, CancellationToken cancellationToken)
        {

            var server = await _serverRepository.GetByIdAsync(request.Id);

            await _serverRepository.RemoveAsync(server);

        }
    }
}
