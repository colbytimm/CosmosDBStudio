﻿using CosmosDBStudio.Model;
using System.Threading.Tasks;

namespace CosmosDBStudio.Services
{
    public interface ICosmosAccountManager
    {
        Task<string[]> GetDatabasesAsync(string accountId);
        Task<string[]> GetContainersAsync(string accountId, string databaseId);

        Task<CosmosDatabase> GetDatabaseAsync(string accountId, string databaseId);
        Task CreateDatabaseAsync(string accountId, CosmosDatabase database);
        Task UpdateDatabaseAsync(string accountId, CosmosDatabase database);
        Task DeleteDatabaseAsync(string accountId, string databaseId);

        Task<CosmosContainer> GetContainerAsync(string accountId, string databaseId, string containerId);
        Task CreateContainerAsync(string accountId, string databaseId, CosmosContainer container);
        Task UpdateContainerAsync(string accountId, string databaseId, CosmosContainer container);
        Task DeleteContainerAsync(string accountId, string databaseId, string containerId);
    }
}