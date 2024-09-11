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
        public sealed class ListByGroupIdEmailListQuery : IRequest<List<EmailList>>
    {
        public ListByGroupIdEmailListQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

        

        public class ListByGroupIdEmailListQueryHandler : IRequestHandler<ListByGroupIdEmailListQuery, List<EmailList>>
        {
            private readonly IEmailListRepository _emailListRepository;

            public ListByGroupIdEmailListQueryHandler(IEmailListRepository emailListRepository)
            {
                _emailListRepository = emailListRepository;
            }

            public async Task<List<EmailList>> Handle(ListByGroupIdEmailListQuery request, CancellationToken cancellationToken)
            {
                return await _emailListRepository.ListByGroupId(request.Id);

            }
        }
    }

}
