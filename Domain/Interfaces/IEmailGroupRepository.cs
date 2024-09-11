using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmailGroupRepository : IRepository<EmailGroup>
    {
        Task<int> GetEmailsCountByUserId(string userId);
        Task<List<EmailGroup>> ListEmailGroupsByUserId(string userId);
        Task<EmailGroup> GetEmailGroupById(long id);
    }
}
