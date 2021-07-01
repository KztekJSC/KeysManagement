using Kztek_Core.Models;
using Kztek_Library.Models;
using Kztek_Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kztek_Service.Admin
{
    public interface ICDKeyService
    {
        Task<GridModel<CDKey>> GetPagingByFirst(string key, int pageNumber, int pageSize);

        Task<CDKey> GetById(string id);

        Task<MessageReport> Create(CDKey obj);

        Task<MessageReport> Update(CDKey obj);

        Task<MessageReport> DeleteById(string id);

        Task<CDKey> GetByCode(string Code);
        Task<IEnumerable<CDKey>> GetAll();
        Task<List<CDKey>> GetByApp(string App);
        Task<List<CDKey>> GetByKeys(string Keys);
    }
}
