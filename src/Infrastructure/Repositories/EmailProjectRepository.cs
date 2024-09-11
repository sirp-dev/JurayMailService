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
       public sealed class EmailProjectRepository : Repository<EmailProject>, IEmailProjectRepository
    {
        private readonly AppDBContext _context;

        public EmailProjectRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<EmailProject>> GetAllByUserId(string userId)
        {
            return await _context.EmailProjects.Where(x => x.AppUserId == userId).ToListAsync();

        }

        public async Task<int> GetProjectCountByUserId(string userId)
        {
            return await _context.EmailProjects.Where(x=>x.AppUserId == userId).CountAsync();
        }
    }
}
