using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Server
    {
        public long Id { get; set; }
        public string? ApiKey { get; set; }
        public string? SenderName { get; set; }
        public string? SenderEmail { get; set; }
        public bool Disable {  get; set; }

        public string? UserId { get; set; }
        public AppUser User { get; set; }
    }
}
