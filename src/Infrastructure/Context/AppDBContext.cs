using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class AppDBContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<EmailList> EmailLists { get; set; }
        public DbSet<EmailProject> EmailProjects { get; set; }
        public DbSet<EmailResponseStatus> EmailResponseStatuses { get; set; }
        public DbSet<EmailSendingStatus> EmailSendingStatuses { get; set; }
        public DbSet<EmailGroup> EmailGroups { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<GroupSendingProject> GroupSendingProjects { get; set; }
    }
}
