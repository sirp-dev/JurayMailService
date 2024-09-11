using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class GetWebHookUpdateIds
    {
        public string UserId { get; set; }
        public long EmailId { get; set; }
    }
}
