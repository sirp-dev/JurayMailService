using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmailListRepository : IRepository<EmailList>
    {
        Task<int> GetEmailsCountByUserId(string userId);
        Task<List<EmailList>> ListByUserId(string userId);
        Task<List<EmailList>> ListByGroupId(long groupId);

        Task AddMany(List<EmailList> emails);
    }
}
