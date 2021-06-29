using Kztek_Core.Models;
using Kztek_Library.Models;
using Kztek_Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kztek_Service.Admin
{
    public interface ICustomerService
    {
        Task<GridModel<Customer>> GetPagingByFirst(string key, int pageNumber, int pageSize);

        Task<Customer> GetById(string id);

        Task<MessageReport> Create(Customer obj);

        Task<MessageReport> Update(Customer obj);

        Task<MessageReport> DeleteById(string id);

        Task<Customer> GetByKey(string phone = "", string email = "");
        Task<IEnumerable<Customer>> GetAll();
    }
}
