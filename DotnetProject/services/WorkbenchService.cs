// Services/WorkbenchService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AasApi.Models;

namespace AasApi.Services
{
    public class WorkbenchService : IWorkbenchService
    {
        private readonly List<Workbench> _workbenches = new List<Workbench>();

        public Task<IEnumerable<Workbench>> GetAllWorkbenchesAsync()
        {
            return Task.FromResult<IEnumerable<Workbench>>(_workbenches);
        }

        public Task<Workbench> GetWorkbenchAsync(string id)
        {
            var workbench = _workbenches.Find(w => w.Id == id) ?? throw new Exception("AasData returned null");
            return Task.FromResult(workbench);
        }

        public Task<bool> SaveWorkbenchAsync(Workbench workbench)
        {
            if (string.IsNullOrEmpty(workbench.Id))
            {
                workbench.Id = Guid.NewGuid().ToString();
            }
            _workbenches.Add(workbench);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteWorkbenchAsync(string id)
        {
            var workbench = _workbenches.Find(w => w.Id == id);
            if (workbench != null)
            {
                _workbenches.Remove(workbench);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
    public interface IWorkbenchService
    {
        Task<IEnumerable<Workbench>> GetAllWorkbenchesAsync();
        Task<Workbench> GetWorkbenchAsync(string id);
        Task<bool> SaveWorkbenchAsync(Workbench workbench);
        Task<bool> DeleteWorkbenchAsync(string id);
    }
}