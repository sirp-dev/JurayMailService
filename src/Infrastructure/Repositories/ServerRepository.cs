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
    public sealed class ServerRepository : Repository<Server>, IServerRepository
    {
        private readonly AppDBContext _context;

        public ServerRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Server>> GetAllByUserId(string userId)
        {
            var data = await _context.Servers.Where(x=>x.UserId == userId).ToListAsync();
            return data;
        }
    }
}
