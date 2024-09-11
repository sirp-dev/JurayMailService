using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class EmailGroup
    {
        public long Id { get; set; }
        public string? Name { get; set; }

        public string? AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<EmailList> Emails { get; set; }
    }
}
