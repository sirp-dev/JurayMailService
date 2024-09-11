using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EmailProjectQueries
{
    public sealed class ListAllByUserIdEmailProjectQuery : IRequest<List<EmailProject>>
    {
        public class ListAllByUserIdEmailProjectQueryHandler : IRequestHandler<ListAllByUserIdEmailProjectQuery, List<EmailProject>>
        {
            private readonly IEmailProjectRepository _emailProjectRepository;

            public ListAllByUserIdEmailProjectQueryHandler(IEmailProjectRepository emailProjectRepository)
            {
                _emailProjectRepository = emailProjectRepository;
            }

            public async Task<List<EmailProject>> Handle(ListAllByUserIdEmailProjectQuery request, CancellationToken cancellationToken)
            {
                return await _emailProjectRepository.GetAllAsync();

            }
        }
    }

}
