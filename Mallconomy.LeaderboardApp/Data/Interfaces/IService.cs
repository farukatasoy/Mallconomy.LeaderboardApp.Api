using System;
using Mallconomy.LeaderboardApp.Data.Entities;

namespace Mallconomy.LeaderboardApp.Data.Interfaces
{
    public interface IService<T> where T : BaseEntity, new()
    {
        Task<List<T>> GetAsync();
        Task<T?> GetAsync(string id);
        Task CreateAsync(T newRecord);
        Task UpdateAsync(string id, T updatedRecord);
        Task RemoveAsync(string id);
    }
}