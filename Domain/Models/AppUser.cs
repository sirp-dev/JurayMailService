using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AppUser : IdentityUser
    {

        [Display(Name = "Fullname")]
        public string? Fullname { get; set; }

        public DateTime Date { get; set; }

    }
}
