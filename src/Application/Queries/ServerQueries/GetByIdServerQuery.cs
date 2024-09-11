using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ServerQueries
{
    public sealed class GetByIdServerQuery : IRequest<Server>
    {
        public GetByIdServerQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

        public class GetByIdServerQueryHandler : IRequestHandler<GetByIdServerQuery, Server>
        {
            private readonly IServerRepository _serverRepository;

            public GetByIdServerQueryHandler(IServerRepository serverRepository)
            {
                _serverRepository = serverRepository;
            }

            public async Task<Server> Handle(GetByIdServerQuery request, CancellationToken cancellationToken)
            {
                return await _serverRepository.GetByIdAsync(request.Id);

            }
        }
    }

}
