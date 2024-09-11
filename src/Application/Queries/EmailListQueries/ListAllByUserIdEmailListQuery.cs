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
    public sealed class ListAllByUserIdEmailListQuery : IRequest<List<EmailList>>
    {


        public string UserId { get; set; }

        public ListAllByUserIdEmailListQuery(string userId)
        {
            UserId = userId;
        }

        public class ListAllByUserIdEmailListQueryHandler : IRequestHandler<ListAllByUserIdEmailListQuery, List<EmailList>>
        {
            private readonly IEmailListRepository _emailListRepository;

            public ListAllByUserIdEmailListQueryHandler(IEmailListRepository emailListRepository)
            {
                _emailListRepository = emailListRepository;
            }

            public async Task<List<EmailList>> Handle(ListAllByUserIdEmailListQuery request, CancellationToken cancellationToken)
            {
                return await _emailListRepository.ListByUserId(request.UserId);

            }
        }
    }

}
