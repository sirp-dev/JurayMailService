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
      public sealed class EmailGroupRepository : Repository<EmailGroup>, IEmailGroupRepository
    {
        private readonly AppDBContext _context;

        public EmailGroupRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EmailGroup> GetEmailGroupById(long id)
        {
            return await _context.EmailGroups
               .Include(x => x.Emails)
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetEmailsCountByUserId(string userId)
        {
            return await _context.EmailGroups.Where(x => x.AppUserId == userId).CountAsync();
        }

        public async Task<List<EmailGroup>> ListEmailGroupsByUserId(string userId)
        {
            return await _context.EmailGroups
                .Include(x=>x.Emails)
                .Where(x => x.AppUserId == userId).ToListAsync();
        }
    }
}
