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
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _CustomerRepository;

        public CustomerService(ICustomerRepository _CustomerRepository)
        {
            this._CustomerRepository = _CustomerRepository;
        }
        public async Task<IEnumerable<Customer>> GetAll()
        {
            var query = from n in _CustomerRepository.Table
                        where !n.IsDeleted
                        select n;

            return await Task.FromResult(query);
        }

        public async Task<GridModel<Customer>> GetPagingByFirst(string key, int pageNumber, int pageSize)
        {
            var query = from n in _CustomerRepository.Table
                        where !n.IsDeleted
                        select n;

            if (!string.IsNullOrWhiteSpace(key))
            {
                query = query.Where(n => n.Name.Contains(key) || n.Phone.Contains(key));
            }

            var pageList = query.ToPagedList(pageNumber, pageSize);

            var model = GridModelHelper<Customer>.GetPage(pageList.OrderByDescending(n => n.DateCreated).ToList(), pageNumber, pageSize, pageList.TotalItemCount, pageList.PageCount);

            return await Task.FromResult(model);

        }

        public async Task<MessageReport> Create(Customer model)
        {
            return await _CustomerRepository.Add(model);
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

        public async Task<Customer> GetById(string id)
        {
            return await _CustomerRepository.GetOneById(id);
        }

        public async Task<MessageReport> Update(Customer model)
        {
            return await _CustomerRepository.Update(model);
        }

        public async Task<Customer> GetByKey(string phone = "",string email = "")
        {
            var query = from n in _CustomerRepository.Table
                        where !n.IsDeleted
                        select n;

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(n => n.Phone.Equals(phone));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(n => n.Email.Equals(email));
            }

            return await Task.FromResult(query.FirstOrDefault());
        }

    }
}
