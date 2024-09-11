using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class EmailProject
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public string? Template { get; set; }
        public string? Subject { get; set; }


        public string? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
