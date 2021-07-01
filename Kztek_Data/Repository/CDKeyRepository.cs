using System;
using Kztek_Data.Infrastructure;
using Kztek_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Kztek_Data.Repository
{
    public interface ICDKeyRepository : IRepository<CDKey>
    {
    }

    public class CDKeyRepository : Repository<CDKey>, ICDKeyRepository
    {
        public CDKeyRepository(DbContextOptions<Kztek_Entities> options) : base(options)
        {
        }
    }
}
