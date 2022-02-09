namespace WorkItems.API.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using Npgsql;
    using WorkItems.API.Entities;

    public class WorkItemRepository : IWorkItemRepository
    {
        private readonly IConfiguration _configuration;

        public WorkItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<WorkItem> GetWorkItem(string assetName)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var assetTest = await connection.QueryFirstOrDefaultAsync<WorkItem>
                ("SELECT * FROM WorkItems WHERE AssetName = @AssetName", new { AssetName = assetName });

            if (assetTest == null)
            {
                return new WorkItem();
            }

            return assetTest;
        }

        public async Task<bool> CreateWorkItem(WorkItem workItem)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("INSERT INTO WorkITem(AssetId, AssetName, WorkType, Description, Status, Passed, DateTime) " +
                "VALUES (@AssetId, @AssetName, @WorkType, @Description, @Status, @Passed, @DateTime)",
                    new
                    {
                        AssetId = workItem.AssetId,
                        AssetName = workItem.AssetName,
                        WorkType = workItem.WorkType,
                        Description = workItem.Description,
                        Status = workItem.Status,
                        Passed = workItem.Passed,
                        DateTime = workItem.DateTime
                    });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateWorkItem(WorkItem workItem)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("UPDATE WorkItem SET AssetName=@AssetName, Description=@Description, Passed=@Passed WHERE Id=@Id",
                    new
                    {
                        AssetName = workItem.AssetName,
                        Description = workItem.Description,
                        Passed = workItem.Passed,
                        Id = workItem.Id
                    });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteWorkItem(string assetName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("DELETE FROM WorkItem WHERE AssetName = @AssetName", new { AssetName = assetName });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
