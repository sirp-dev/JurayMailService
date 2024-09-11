using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EmailListQueries
{
    public sealed class GetByIdEmailListQuery : IRequest<EmailList>
    {
        public GetByIdEmailListQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

        public class GetByIdEmailListQueryHandler : IRequestHandler<GetByIdEmailListQuery, EmailList>
        {
            private readonly IEmailListRepository _emailListRepository;

            public GetByIdEmailListQueryHandler(IEmailListRepository emailListRepository)
            {
                _emailListRepository = emailListRepository;
            }

            public async Task<EmailList> Handle(GetByIdEmailListQuery request, CancellationToken cancellationToken)
            {
                return await _emailListRepository.GetByIdAsync(request.Id);

            }
        }
    }

}
