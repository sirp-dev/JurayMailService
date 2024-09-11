using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enum.EnumStatus;

namespace Domain.Models
{
    public class EmailResponseStatus
    {
        public long Id { get; set; }

        public long? EmailListId { get; set; }
        public EmailList EmailList { get; set; }



        public DateTime? SentDate { get; set; }
        public string? RecordType { get; set; }
        public string? Log { get; set; }
        public string? MessageId { get; set; }
        public string? UserId { get; set; }
        public AppUser User { get; set; }
    }
}
