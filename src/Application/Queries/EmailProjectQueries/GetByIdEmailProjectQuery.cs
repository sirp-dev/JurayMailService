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
    public sealed class GetByIdEmailProjectQuery : IRequest<EmailProject>
    {
        public GetByIdEmailProjectQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

        public class GetByIdEmailProjectQueryHandler : IRequestHandler<GetByIdEmailProjectQuery, EmailProject>
        {
            private readonly IEmailProjectRepository _emailProjectRepository;

            public GetByIdEmailProjectQueryHandler(IEmailProjectRepository emailProjectRepository)
            {
                _emailProjectRepository = emailProjectRepository;
            }

            public async Task<EmailProject> Handle(GetByIdEmailProjectQuery request, CancellationToken cancellationToken)
            {
                return await _emailProjectRepository.GetByIdAsync(request.Id);

            }
        }
    }

}
