namespace WorkItems.API.Repositories
{
    using System.Threading.Tasks;
    using WorkItems.API.Entities;

    public interface IWorkItemRepository
    {
        Task<bool> CreateWorkItem(WorkItem workItem);
        Task<bool> DeleteWorkItem(string assetName);
        Task<WorkItem> GetWorkItem(string assetName);
        Task<bool> UpdateWorkItem(WorkItem workItem);
    }
}