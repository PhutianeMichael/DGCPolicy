using DgcPolicyApi.Models;

namespace DgcPolicyApi.Services
{
    public interface IPolicyRepository
    {
        Task<Policy> GetAsync(string id);
        Task CreateAsync(Policy policy);
        Task<List<Policy>> GetAsync();
        Task UpdateAsync(string id,Policy policy);
        Task RemoveAsync(string id);
    }
}