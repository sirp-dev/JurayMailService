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
    public sealed class EmailResponseStatusRepository : Repository<EmailResponseStatus>, IEmailResponseStatusRepository
    {
        private readonly AppDBContext _context;

        public EmailResponseStatusRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<EmailResponseStatus>> GetAllResponseByMessageId(string messageId)
        {
           var list = await _context.EmailResponseStatuses 
                .Where(x=>x.MessageId == messageId).ToListAsync();

            return list;
        }
    }
}
