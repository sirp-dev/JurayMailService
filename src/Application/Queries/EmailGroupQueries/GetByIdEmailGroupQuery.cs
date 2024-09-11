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
    public sealed class GetByIdEmailGroupQuery : IRequest<EmailGroup>
    {
        public GetByIdEmailGroupQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

        public class GetByIdEmailGroupQueryHandler : IRequestHandler<GetByIdEmailGroupQuery, EmailGroup>
        {
            private readonly IEmailGroupRepository _emailGroupRepository;

            public GetByIdEmailGroupQueryHandler(IEmailGroupRepository emailGroupRepository)
            {
                _emailGroupRepository = emailGroupRepository;
            }

            public async Task<EmailGroup> Handle(GetByIdEmailGroupQuery request, CancellationToken cancellationToken)
            {
                return await _emailGroupRepository.GetEmailGroupById(request.Id);

            }
        }
    }

}
