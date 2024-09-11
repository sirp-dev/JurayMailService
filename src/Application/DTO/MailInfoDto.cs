using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class MailInfoDto
    {

        public EmailSendingStatus EmailSendingStatus { get; set; }
        public List<EmailResponseStatus> EmailResponseStatusList { get; set; }
    }
}
