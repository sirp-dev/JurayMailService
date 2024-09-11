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
    public sealed class ListAllEmailResponseStatusQuery : IRequest<List<EmailResponseStatus>>
    {
        public class ListAllEmailResponseStatusQueryHandler : IRequestHandler<ListAllEmailResponseStatusQuery, List<EmailResponseStatus>>
        {
            private readonly IEmailResponseStatusRepository _emailResponseStatusRepository;

            public ListAllEmailResponseStatusQueryHandler(IEmailResponseStatusRepository emailResponseStatusRepository)
            {
                _emailResponseStatusRepository = emailResponseStatusRepository;
            }

            public async Task<List<EmailResponseStatus>> Handle(ListAllEmailResponseStatusQuery request, CancellationToken cancellationToken)
            {
                return await _emailResponseStatusRepository.GetAllAsync();

            }
        }
    }

}
