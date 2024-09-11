using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{  
    public interface IGroupSendingProjectRepository : IRepository<GroupSendingProject>
    {
        Task<List<GroupSendingProject>> GetGroupsByProject(long projectId);
    }
}
