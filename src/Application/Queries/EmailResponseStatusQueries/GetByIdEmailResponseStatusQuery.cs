using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EmailResponseStatusQueries
{
    public sealed class GetByIdEmailResponseStatusQuery : IRequest<EmailResponseStatus>
    {
        public GetByIdEmailResponseStatusQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

        public class GetByIdEmailResponseStatusQueryHandler : IRequestHandler<GetByIdEmailResponseStatusQuery, EmailResponseStatus>
        {
            private readonly IEmailResponseStatusRepository _emailResponseStatusRepository;

            public GetByIdEmailResponseStatusQueryHandler(IEmailResponseStatusRepository emailResponseStatusRepository)
            {
                _emailResponseStatusRepository = emailResponseStatusRepository;
            }

            public async Task<EmailResponseStatus> Handle(GetByIdEmailResponseStatusQuery request, CancellationToken cancellationToken)
            {
                return await _emailResponseStatusRepository.GetByIdAsync(request.Id);

            }
        }
    }

}
