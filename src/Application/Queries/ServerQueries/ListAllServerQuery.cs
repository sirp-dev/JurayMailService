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
    public sealed class ListAllServerQuery : IRequest<List<Server>>
    {
 

        public class ListAllServerQueryHandler : IRequestHandler<ListAllServerQuery, List<Server>>
        {
            private readonly IServerRepository _serverRepository;

            public ListAllServerQueryHandler(IServerRepository serverRepository)
            {
                _serverRepository = serverRepository;
            }

            public async Task<List<Server>> Handle(ListAllServerQuery request, CancellationToken cancellationToken)
            {
                return await _serverRepository.GetAllAsync();

            }
        }
    }
}
