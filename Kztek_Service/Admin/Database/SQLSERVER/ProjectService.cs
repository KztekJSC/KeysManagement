﻿using Kztek_Core.Models;
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
    public class ProjectService : IProjectService
    {
        private IProjectRepository _ProjectRepository;

        public ProjectService(IProjectRepository _ProjectRepository)
        {
            this._ProjectRepository = _ProjectRepository;
        }
        public async Task<IEnumerable<Project>> GetAll()
        {
            var query = from n in _ProjectRepository.Table
                        where !n.IsDeleted
                        select n;

            return await Task.FromResult(query);
        }

        public async Task<GridModel<Project>> GetPagingByFirst(string key, int pageNumber, int pageSize)
        {
            var query = from n in _ProjectRepository.Table
                        where !n.IsDeleted
                        select n;

            if (!string.IsNullOrWhiteSpace(key))
            {
                query = query.Where(n => n.Name.Contains(key));
            }

            var pageList = query.ToPagedList(pageNumber, pageSize);

            var model = GridModelHelper<Project>.GetPage(pageList.OrderByDescending(n => n.DateCreated).ToList(), pageNumber, pageSize, pageList.TotalItemCount, pageList.PageCount);

            return await Task.FromResult(model);

        }

        public async Task<MessageReport> Create(Project model)
        {
            return await _ProjectRepository.Add(model);
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

        public async Task<Project> GetById(string id)
        {
            return await _ProjectRepository.GetOneById(id);
        }

        public async Task<MessageReport> Update(Project model)
        {
            return await _ProjectRepository.Update(model);
        }

        public async Task<Project> GetByName(string name)
        {
            var query = from n in _ProjectRepository.Table
                        where n.Name == name && !n.IsDeleted
                        select n;


            return await Task.FromResult(query.FirstOrDefault());
        }

    }
}
