using Application.DTO;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.DashboardQueries
{
    public sealed class GetDashboardQuery : IRequest<DashboardDto>
    {
        public GetDashboardQuery(string? userId)
        {
            UserId = userId;
        }

        public string? UserId { get; set; }


        public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardDto>
        {
            private readonly IEmailListRepository _emailListRepository;
            private readonly IEmailProjectRepository _emailProjectRepository;
            public GetDashboardQueryHandler(IEmailListRepository emailListRepository, IEmailProjectRepository emailProjectRepository)
            {
                _emailListRepository = emailListRepository;
                _emailProjectRepository = emailProjectRepository;
            }

            public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
            {
                DashboardDto dashboard = new DashboardDto();

                //get project
                dashboard.AllProjects = await _emailProjectRepository.GetProjectCountByUserId(request.UserId);
                dashboard.AllEmails = await _emailListRepository.GetEmailsCountByUserId(request.UserId);

                //emails


                return dashboard;

            }
        }
    }

}
