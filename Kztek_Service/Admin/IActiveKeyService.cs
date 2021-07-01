using Kztek_Core.Models;
using Kztek_Library.Models;
using Kztek_Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kztek_Service.Admin
{
    public interface IActiveKeyService
    {
        Task<GridModel<ActiveKey>> GetPagingByFirst(string key, int pageNumber, int pageSize);

        Task<ActiveKey> GetById(string id);

        Task<MessageReport> Create(ActiveKey obj);

        Task<MessageReport> Update(ActiveKey obj);

        Task<MessageReport> DeleteById(string id);

        Task<ActiveKey> GetByKeyActive(string KeyActive);
        Task<IEnumerable<ActiveKey>> GetAll();
    }
}
