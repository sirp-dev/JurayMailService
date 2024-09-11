using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enum.EnumStatus;

namespace Domain.Models
{
    public class EmailSendingStatus
    {
        public long Id { get; set; }

        public string Group { get; set; }

        public long? EmailListId { get; set; }
        public EmailList EmailList {  get; set; }


        public long EmailProjectId { get; set; }
        public EmailProject EmailProject { get; set; }

        public string? UserId { get; set; }
        public AppUser User { get; set; }

        public DateTime SubmittedDate { get; set; }
        public DateTime? SentDate { get; set; }
        public SendingStatus SendingStatus { get;set;   }
        public int Retries { get; set; }

        public string? Log {  get; set; }
        public string? MessageId { get; set; }
    }
}
