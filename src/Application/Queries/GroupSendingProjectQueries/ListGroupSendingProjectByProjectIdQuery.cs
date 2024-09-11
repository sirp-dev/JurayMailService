using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GroupSendingProjectQueries
{
    public sealed class ListGroupSendingProjectByProjectIdQuery : IRequest<List<GroupSendingProject>>
    {
        public ListGroupSendingProjectByProjectIdQuery(long projectId)
        {
            ProjectId = projectId;
        }

        public long ProjectId { get; set; }

     

        public class ListGroupSendingProjectByProjectIdQueryHandler : IRequestHandler<ListGroupSendingProjectByProjectIdQuery, List<GroupSendingProject>>
        {
            private readonly IGroupSendingProjectRepository _groupSendingProjectRepository;

            public ListGroupSendingProjectByProjectIdQueryHandler(IGroupSendingProjectRepository groupSendingProjectRepository)
            {
                _groupSendingProjectRepository = groupSendingProjectRepository;
            }

            public async Task<List<GroupSendingProject>> Handle(ListGroupSendingProjectByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var data = await _groupSendingProjectRepository.GetGroupsByProject(request.ProjectId);
                return data;
            }
        }
    }
}
