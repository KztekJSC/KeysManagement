using Kztek_Core.Models;
using Kztek_Library.Models;
using Kztek_Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kztek_Service.Admin
{
    public interface IProjectService
    {
        Task<GridModel<Project>> GetPagingByFirst(string key, int pageNumber, int pageSize);

        Task<Project> GetById(string id);

        Task<MessageReport> Create(Project obj);

        Task<MessageReport> Update(Project obj);

        Task<MessageReport> DeleteById(string id);

        Task<Project> GetByName(string name);
        Task<IEnumerable<Project>> GetAll();
    }
}
