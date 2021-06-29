using System;
using Kztek_Data.Infrastructure;
using Kztek_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Kztek_Data.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
    }

    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContextOptions<Kztek_Entities> options) : base(options)
        {
        }
    }
}
