using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class GroupSendingProject
    {
        public long Id { get; set; }

        public long EmailProjectId { get; set; }
        public EmailProject EmailProject { get; set; }

        public long? EmailGroupId { get; set; }
        public EmailGroup EmailGroup { get; set; }

        public bool AllGroups {  get; set; }
        public int Submitted { get; set; }
        public DateTime Date {  get; set; }
    }
}
