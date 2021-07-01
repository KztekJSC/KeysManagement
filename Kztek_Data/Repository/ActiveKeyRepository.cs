using System;
using Kztek_Data.Infrastructure;
using Kztek_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Kztek_Data.Repository
{
    public interface IActiveKeyRepository : IRepository<ActiveKey>
    {
    }

    public class ActiveKeyRepository : Repository<ActiveKey>, IActiveKeyRepository
    {
        public ActiveKeyRepository(DbContextOptions<Kztek_Entities> options) : base(options)
        {
        }
    }
}
