using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EmailGroupQueries
{
    public sealed class ListAllByUserIdEmailGroupQuery : IRequest<List<EmailGroup>>
    {
        public ListAllByUserIdEmailGroupQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
        public class ListAllByUserIdEmailGroupQueryHandler : IRequestHandler<ListAllByUserIdEmailGroupQuery, List<EmailGroup>>
        {
            private readonly IEmailGroupRepository _emailGroupRepository;

            public ListAllByUserIdEmailGroupQueryHandler(IEmailGroupRepository emailGroupRepository)
            {
                _emailGroupRepository = emailGroupRepository;
            }

            public async Task<List<EmailGroup>> Handle(ListAllByUserIdEmailGroupQuery request, CancellationToken cancellationToken)
            {
                return await _emailGroupRepository.ListEmailGroupsByUserId(request.UserId);

            }
        }
    }

}
