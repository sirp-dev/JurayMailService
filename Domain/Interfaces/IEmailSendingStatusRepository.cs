using Domain.DTO;
using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmailSendingStatusRepository : IRepository<EmailSendingStatus>
    {
        Task<RegisterGroupEmailsDto> AddEmailsForSending(long projectId, long? groupId, bool sendToAllGroup);
        Task<List<EmailSendingStatus>> ListByUserId(string userId);

        Task<IEnumerable<EmailSendingStatus>> GetListByUserIdAsync(int pageNumber, int pageSize, string userId);
        Task<IEnumerable<EmailResponseStatus>> GetResponseListByUserIdAsync(int pageNumber, int pageSize, string userId);

        Task<int> GetTotalCountByUserIdAsync(string userId);
        Task<int> GetResponseTotalCountByUserIdAsync(string userId);

        Task SendBatchEmailByEmailIds();

        Task<GetWebHookUpdateIds> GetEmailListIdByMessageId(string  messageId);

        Task<EmailSendingStatus> GetEmailSendingById(long emailId);
    }
}
