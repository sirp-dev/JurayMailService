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
    public sealed class ListProjectByUserId : IRequest<List<EmailProject>>
    {
        public ListProjectByUserId(string? userId)
        {
            UserId = userId;
        }

        public string? UserId { get; set; }

        public class ListProjectByUserIdHandler : IRequestHandler<ListProjectByUserId, List<EmailProject>>
        {
            private readonly IEmailProjectRepository _emailProjectRepository;

            public ListProjectByUserIdHandler(IEmailProjectRepository emailProjectRepository)
            {
                _emailProjectRepository = emailProjectRepository;
            }

            public async Task<List<EmailProject>> Handle(ListProjectByUserId request, CancellationToken cancellationToken)
            {
                return await _emailProjectRepository.GetAllByUserId(request.UserId);

            }
        }
    }

}
