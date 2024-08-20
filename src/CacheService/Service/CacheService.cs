using System.Text.Json;
using Application.Persistence.Services.CacheService;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace CacheServices.Service;

public class CacheService: ICacheService
{
    private readonly IDatabase _cacheDb;
    private readonly IConfiguration _configuration;
    
    
    public CacheService(IConfiguration configuration)
    {
        _configuration = configuration;
        var redis = ConnectionMultiplexer.Connect(_configuration["Redis:Connection"]);
        _cacheDb = redis.GetDatabase();
    }
    public T GetData<T>(string key)
    {
        var value = _cacheDb.StringGet(key);
        
        if (!string.IsNullOrEmpty(value))
        {
            return JsonSerializer.Deserialize<T>(value);
        }
        return default;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var expireTime = expirationTime.DateTime.Subtract(DateTime.Now);
        var isSet = _cacheDb.StringSet(key,JsonSerializer.Serialize(value),expireTime);
        return isSet;
    }

    public object RemoveData(string key)
    {
        var exist = _cacheDb.KeyExists(key);
        if (exist)
        {
            _cacheDb.KeyDelete(key);
        }

        return false;
    }
}