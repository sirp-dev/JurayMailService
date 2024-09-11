using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ServerCommands
{
    public sealed class UpdateServerCommand : IRequest
    {
        public UpdateServerCommand(Server server)
        {
            Server = server;
        }

        public Server Server { get; set; }


    }

    public class UpdateServerCommandHandler : IRequestHandler<UpdateServerCommand>
    {
        private readonly IServerRepository _serverRepository;

        public UpdateServerCommandHandler(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task Handle(UpdateServerCommand request, CancellationToken cancellationToken)
        {

            await _serverRepository.UpdateAsync(request.Server);
        }
    }
}
