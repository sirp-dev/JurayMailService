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

    public sealed class EmailListRepository : Repository<EmailList>, IEmailListRepository
    {
        private readonly AppDBContext _context;

        public EmailListRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddMany(List<EmailList> emails)
        {
            foreach (var email in emails)
            {
                // Check if the email already exists in the database
                bool isDuplicate = await _context.EmailLists.AnyAsync(e => e.Email == email.Email && e.EmailGroupId == email.EmailGroupId);

                // If the email is not a duplicate, add it to the database
                if (!isDuplicate)
                {
                    await _context.EmailLists.AddAsync(email);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetEmailsCountByUserId(string userId)
        {
            return await _context.EmailLists.Where(x => x.AppUserId == userId).CountAsync();
        }

        public async Task<List<EmailList>> ListByGroupId(long groupId)
        {
            return await _context.EmailLists.Where(x => x.EmailGroupId == groupId).ToListAsync();
        }

        public async Task<List<EmailList>> ListByUserId(string userId)
        {
            return await _context.EmailLists.Where(x => x.AppUserId == userId).ToListAsync();
        }
    }
}
