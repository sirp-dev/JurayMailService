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
    public sealed class AddServerCommand : IRequest
    {
        public AddServerCommand(Server server)
        {
            Server = server;
        }

        public Server Server { get; set; }


    }

    public class AddServerCommandHandler : IRequestHandler<AddServerCommand>
    {
        private readonly IServerRepository _serverRepository;

        public AddServerCommandHandler(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task Handle(AddServerCommand request, CancellationToken cancellationToken)
        {

            await _serverRepository.AddAsync(request.Server);


        }
    }
}
