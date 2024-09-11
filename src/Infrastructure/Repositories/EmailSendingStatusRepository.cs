using Azure;
using Domain.DTO;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PostmarkEmailService;

namespace Infrastructure.Repositories
{
    public sealed class EmailSendingStatusRepository : Repository<EmailSendingStatus>, IEmailSendingStatusRepository
    {
        private readonly AppDBContext _context;
        private readonly PostmarkClient _postmarkClient;
        private readonly IConfiguration _configuration;

        public EmailSendingStatusRepository(AppDBContext context, PostmarkClient postmarkClient, IConfiguration configuration) : base(context)
        {
            _context = context;
            _postmarkClient = new PostmarkClient(GetServerTokenFromSettings());
            _configuration = configuration;
        }
        private string GetServerTokenFromSettings()
        {
            return "cbd0e0f4-d49d-4f7e-9412-9e741a8f8705";
            //return _configuration.GetSection("PostmarkSettings")["ServerToken"];
        }
        public async Task<RegisterGroupEmailsDto> AddEmailsForSending(long projectId, long? groupId, bool sendToAllGroup)
        {


            RegisterGroupEmailsDto result = new RegisterGroupEmailsDto();
            string groupname = "";

            try
            {
                var getProject = await _context.EmailProjects.FindAsync(projectId);

                if (getProject != null)
                {
                    if (sendToAllGroup)
                    {
                        var allEmails = await _context.EmailLists
                            .Where(x => x.AppUserId == getProject.AppUserId)
                            .Distinct()
                            .ToListAsync();

                        groupname = "All Groups";

                        await AddEmailSendingStatuses(allEmails, groupname, getProject.Id, null);

                        result.Submitted = allEmails.Count;
                        result.Success = true;
                    }
                    else
                    {
                        var groupemails = await _context.EmailGroups.FindAsync(groupId);

                        if (groupemails != null)
                        {
                            var groupEmails = await _context.EmailLists
                                .Where(x => x.EmailGroupId == groupId)
                                .Distinct()
                                .ToListAsync();

                            groupname = groupemails.Name;

                            await AddEmailSendingStatuses(groupEmails, groupname, getProject.Id, groupId);

                            result.Submitted = groupEmails.Count;
                            result.Success = true;


                        }
                    }

                    //

                }
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public async Task<IEnumerable<EmailSendingStatus>> GetListByUserIdAsync(int pageNumber, int pageSize, string userId)
        {
            var list = _context.EmailSendingStatuses
                .Include(x => x.EmailList)
                .Where(x => x.UserId == userId)
                .OrderByDescending(e => e.SubmittedDate)
    .Skip(pageSize * (pageNumber - 1)) // Calculate the offset based on the page number
    .Take(pageSize); // Take only the specified number of records per page


            //.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return list;
        }

        public async Task<int> GetTotalCountByUserIdAsync(string userId)
        {
            return await _context.EmailSendingStatuses.Where(x => x.UserId == userId).CountAsync();
        }

        public async Task<List<EmailSendingStatus>> ListByUserId(string userId)
        {
            var list = await _context.EmailSendingStatuses.Where(x => x.UserId == userId).ToListAsync();
            return list;
        }

        public async Task SendBatchEmailByEmailIds()
        {
            var getemails = await _context.EmailSendingStatuses
                .AsNoTracking()
                .Include(x => x.EmailList)
                .Include(x => x.EmailProject)
                .Where(x => x.SendingStatus == Domain.Enum.EnumStatus.SendingStatus.Pending &&
            x.Retries <= 5).Take(30).ToListAsync();

            foreach (var ms in getemails)
            {
                //get sender email and name
                var server = await _context.Servers.FirstOrDefaultAsync(x=>x.UserId == ms.UserId && x.Disable == false);
                if(server == null)
                {
                    continue;
                }
                //replace the name of the user in the email template.
                string emailbody = ms.EmailProject.Template.Replace("$$name$$", ms.EmailList.Name);
                //get the email to be update after sending request.
                var updateSenderMail = await _context.EmailSendingStatuses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == ms.Id);
                try
                {
                     
                    var message = new PostmarkMessage
                    {
                        From = $"{server.SenderName} <{server.SenderEmail}>",
                        To = ms.EmailList.Email,
                        Subject = ms.EmailProject.Subject,
                        HtmlBody = emailbody
                    };

                    PostmarkResponse response = await _postmarkClient.SendMessageAsync(message);
                    
                    if (response.Status == 0)
                    {
                        updateSenderMail.SendingStatus = Domain.Enum.EnumStatus.SendingStatus.Sent;
                        updateSenderMail.SentDate = DateTime.UtcNow.AddHours(1);
                        updateSenderMail.Log =  response.Message + $"({response.MessageID})";
                        updateSenderMail.MessageId = response.MessageID.ToString();

                        _context.Entry(updateSenderMail).State = EntityState.Modified;
                    }
                    else
                    {
                        updateSenderMail.SendingStatus = Domain.Enum.EnumStatus.SendingStatus.Failed;
                    updateSenderMail.Retries += 1;
                        updateSenderMail.Log = response.Message + $"({response.MessageID})";
                        updateSenderMail.MessageId = response.MessageID.ToString();

                        _context.Entry(updateSenderMail).State = EntityState.Modified;
                    }

                }
                catch (Exception ex)
                {
                    updateSenderMail.SendingStatus = Domain.Enum.EnumStatus.SendingStatus.Failed;
                    updateSenderMail.Retries += 1;
                    updateSenderMail.Log = ex.ToString();

                    _context.Entry(updateSenderMail).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();

        }

        private async Task AddEmailSendingStatuses(IEnumerable<EmailList> emails, string groupname, long projectId, long? groupId)
        {
            var distinctEmails = emails.Select(x => x.Email).Distinct();
            int count = 0;
            foreach (var email in distinctEmails)
            {
                EmailSendingStatus sendmail = new EmailSendingStatus
                {
                    SendingStatus = Domain.Enum.EnumStatus.SendingStatus.Pending,
                    Retries = 0,
                    EmailListId = emails.FirstOrDefault(x => x.Email == email).Id,
                    Group = groupname,
                    EmailProjectId = projectId,
                    UserId = emails.FirstOrDefault(x => x.Email == email).AppUserId,
                    SubmittedDate = DateTime.UtcNow.AddHours(1),
                };
                count++;
                await _context.EmailSendingStatuses.AddAsync(sendmail);
            }
            GroupSendingProject groupSendingProject = new GroupSendingProject();
            groupSendingProject.Submitted = count;
            groupSendingProject.EmailProjectId = projectId;
            if (groupId == null)
            {
                groupSendingProject.AllGroups = true;
            }
            else
            {
                groupSendingProject.EmailGroupId = groupId;
            }
            groupSendingProject.Date = DateTime.UtcNow.AddHours(1);
            await _context.GroupSendingProjects.AddAsync(groupSendingProject);
            await _context.SaveChangesAsync();
        }

        public async Task<GetWebHookUpdateIds> GetEmailListIdByMessageId(string messageId)
        {
            GetWebHookUpdateIds result = new GetWebHookUpdateIds();
            var data = await _context.EmailSendingStatuses.FirstOrDefaultAsync(x=>x.MessageId == messageId);
            result.EmailId = data.EmailListId ?? 0; 
            result.UserId = data.UserId;
            return result;
        }

        public async Task<EmailSendingStatus> GetEmailSendingById(long emailId)
        {
           var data = await _context.EmailSendingStatuses
                .Include(x=>x.EmailList)
                .Include(x=>x.EmailProject)
                .FirstOrDefaultAsync(x=>x.Id == emailId);

            return data;
        }

        public async Task<IEnumerable<EmailResponseStatus>> GetResponseListByUserIdAsync(int pageNumber, int pageSize, string userId)
        {
            var list = _context.EmailResponseStatuses
                 .Include(x => x.EmailList)
                 .Where(x => x.UserId == userId)
                 .OrderByDescending(e => e.SentDate)
     .Skip(pageSize * (pageNumber - 1)) // Calculate the offset based on the page number
     .Take(pageSize); // Take only the specified number of records per page


            //.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return list;
        }

        public async Task<int> GetResponseTotalCountByUserIdAsync(string userId)
        {
            return await _context.EmailResponseStatuses.Where(x => x.UserId == userId).CountAsync();
        }
    }
}
