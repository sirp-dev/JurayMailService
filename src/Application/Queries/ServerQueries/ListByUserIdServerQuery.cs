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
    public sealed class ListByUserIdServerQuery : IRequest<List<Server>>
    {

        public string UserId { get; set; }

        public ListByUserIdServerQuery(string userId)
        {
            UserId = userId;
        }

        public class ListByUserIdServerQueryHandler : IRequestHandler<ListByUserIdServerQuery, List<Server>>
        {
            private readonly IServerRepository _serverRepository;

            public ListByUserIdServerQueryHandler(IServerRepository serverRepository)
            {
                _serverRepository = serverRepository;
            }

            public async Task<List<Server>> Handle(ListByUserIdServerQuery request, CancellationToken cancellationToken)
            {
                var data = await _serverRepository.GetAllByUserId(request.UserId);
                return data;
            }
        }
    }

}
