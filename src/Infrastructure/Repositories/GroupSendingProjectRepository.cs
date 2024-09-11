using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public sealed class GroupSendingProjectRepository : Repository<GroupSendingProject>, IGroupSendingProjectRepository
    {
        private readonly AppDBContext _context;

        public GroupSendingProjectRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<GroupSendingProject>> GetGroupsByProject(long projectId)
        {
           return await _context.GroupSendingProjects.Where(x=>x.EmailProjectId == projectId).ToListAsync();
        }
    }
}
