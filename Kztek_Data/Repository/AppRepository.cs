using System;
using Kztek_Data.Infrastructure;
using Kztek_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Kztek_Data.Repository
{
    public interface IAppRepository : IRepository<App>
    {
    }

    public class AppRepository : Repository<App>, IAppRepository
    {
        public AppRepository(DbContextOptions<Kztek_Entities> options) : base(options)
        {
        }
    }
}
