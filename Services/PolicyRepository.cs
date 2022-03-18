using DgcPolicyApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DgcPolicyApi.Services;
public class PolicyRepository: IPolicyRepository
{
    private readonly IMongoCollection<Policy> _policyCollection;

    public PolicyRepository(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _policyCollection = database.GetCollection<Policy>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<Policy>> GetAsync() => await _policyCollection.Find(_ => true).ToListAsync();
    public async Task<Policy> GetAsync(string id) => await _policyCollection.Find(policy => policy.Id == id).FirstOrDefaultAsync();
    
    public async Task CreateAsync(Policy policy) => await _policyCollection.InsertOneAsync(policy);

    public async Task UpdateAsync(string id, Policy policy) => await _policyCollection.ReplaceOneAsync(policy => policy.Id == id, policy);

    public async Task RemoveAsync(string id) => await _policyCollection.DeleteOneAsync(policy => policy.Id == id);

    
}
