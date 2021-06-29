using System;
using Kztek_Data.Infrastructure;
using Kztek_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Kztek_Data.Repository
{
    public interface IProjectRepository : IRepository<Project>
    {
    }

    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContextOptions<Kztek_Entities> options) : base(options)
        {
        }
    }
}
