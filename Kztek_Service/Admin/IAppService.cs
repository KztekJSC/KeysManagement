using Kztek_Core.Models;
using Kztek_Library.Models;
using Kztek_Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kztek_Service.Admin
{
    public interface IAppService
    {
        Task<GridModel<App>> GetPagingByFirst(string key, int pageNumber, int pageSize);

        Task<App> GetById(string id);

        Task<MessageReport> Create(App obj);

        Task<MessageReport> Update(App obj);

        Task<MessageReport> DeleteById(string id);

        Task<App> GetByName(string name);
        Task<IEnumerable<App>> GetAll();
    }
}
