using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class EmailList
    {
        public long Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }


        public string? AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public long EmailGroupId { get; set; }
        public EmailGroup EmailGroup { get; set; }
    }
}
