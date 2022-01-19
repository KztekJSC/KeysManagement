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
    public class ActiveKeyService : IActiveKeyService
    {
        private IActiveKeyRepository _ActiveKeyRepository;

        public ActiveKeyService(IActiveKeyRepository _ActiveKeyRepository)
        {
            this._ActiveKeyRepository = _ActiveKeyRepository;
        }
        public async Task<IEnumerable<ActiveKey>> GetAll()
        {
            var query = from n in _ActiveKeyRepository.Table
                        where !n.IsDeleted
                        select n;

            return await Task.FromResult(query);
        }

        public async Task<GridModel<ActiveKey>> GetPagingByFirst(string key, int pageNumber, int pageSize)
        {
            var query = from n in _ActiveKeyRepository.Table
                        where !n.IsDeleted
                        select n;

            if (!string.IsNullOrWhiteSpace(key))
            {
                query = query.Where(n => n.KeyActive.Contains(key));
            }

            var pageList = query.OrderByDescending(n => n.DateCreated).ToPagedList(pageNumber, pageSize);

            var model = GridModelHelper<ActiveKey>.GetPage(pageList.ToList(), pageNumber, pageSize, pageList.TotalItemCount, pageList.PageCount);

            return await Task.FromResult(model);

        }

        public async Task<MessageReport> Create(ActiveKey model)
        {
            return await _ActiveKeyRepository.Add(model);
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

        public async Task<ActiveKey> GetById(string id)
        {
            return await _ActiveKeyRepository.GetOneById(id);
        }

        public async Task<MessageReport> Update(ActiveKey model)
        {
            return await _ActiveKeyRepository.Update(model);
        }

        public async Task<ActiveKey> GetByKeyActive(string KeyActive)
        {
            var query = from n in _ActiveKeyRepository.Table
                        where n.KeyActive == KeyActive && !n.IsDeleted
                        select n;


            return await Task.FromResult(query.FirstOrDefault());
        }
        public async Task<List<ActiveKey>> GetByApp(string cdkeys)
        {
            var query = from n in _ActiveKeyRepository.Table
                        where !n.IsDeleted
                        select n;

            if (!string.IsNullOrEmpty(cdkeys))
            {
                var arr = cdkeys.Split(',').Where(n => !string.IsNullOrEmpty(n));

                query = query.Where(n => arr.Contains(n.CDKey));
            }

            return await Task.FromResult(query.ToList());
        }
    }
}
