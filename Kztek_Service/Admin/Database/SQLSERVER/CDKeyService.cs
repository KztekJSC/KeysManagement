using Kztek_Core.Models;
using Kztek_Data.Repository;
using Kztek_Library.Helpers;
using Kztek_Library.Models;
using Kztek_Model.Models;
using Kztek_Service.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Kztek_Service.Admin.Database.SQLSERVER
{
    public class CDKeyService : ICDKeyService
    {
        private ICDKeyRepository _CDKeyRepository;

        public CDKeyService(ICDKeyRepository _CDKeyRepository)
        {
            this._CDKeyRepository = _CDKeyRepository;
        }
        public async Task<IEnumerable<CDKey>> GetAll()
        {
            var query = from n in _CDKeyRepository.Table
                        where !n.IsDeleted
                        select n;

            return await Task.FromResult(query);
        }

        public async Task<GridModel<CDKey>> GetPagingByFirst(string key, int pageNumber, int pageSize)
        {
            var query = from n in _CDKeyRepository.Table
                        where !n.IsDeleted
                        select n;

            if (!string.IsNullOrWhiteSpace(key))
            {
                query = query.Where(n => n.Code.Contains(key));
            }

            var pageList = query.ToPagedList(pageNumber, pageSize);

            var model = GridModelHelper<CDKey>.GetPage(pageList.OrderByDescending(n => n.DateCreated).ToList(), pageNumber, pageSize, pageList.TotalItemCount, pageList.PageCount);

            return await Task.FromResult(model);

        }

        public async Task<MessageReport> Create(CDKey model)
        {
            return await _CDKeyRepository.Add(model);
        }

        public async Task<MessageReport> DeleteById(string id)
        {
            var result = new MessageReport(false, await LanguageHelper.GetLanguageText("MESSAGEREPORT:ERR"));

            var obj = await GetById(id);
            if (obj != null)
            {
                obj.IsDeleted = true;
                result = await Update(obj);
            }
            else
            {
                result = new MessageReport(false, await LanguageHelper.GetLanguageText("MESSAGEREPORT:NON_RECORD"));
            }

            return await Task.FromResult(result);
        }

        public async Task<CDKey> GetById(string id)
        {
            return await _CDKeyRepository.GetOneById(id);
        }

        public async Task<MessageReport> Update(CDKey model)
        {
            return await _CDKeyRepository.Update(model);
        }

        public async Task<CDKey> GetByCode(string Code)
        {
            var query = from n in _CDKeyRepository.Table
                        where n.Code == Code && !n.IsDeleted
                        select n;


            return await Task.FromResult(query.FirstOrDefault());
        }

    }
}
