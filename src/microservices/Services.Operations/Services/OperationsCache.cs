using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Bson.IO;
using Services.Operations.Dtos;
using Services.RabbitMq.Interfaces.Messaging;
using Services.RabbitMq.Messages;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Services.Operations.Services
{
    public class OperationsCache : IOperationsCache
    {
        private readonly IDistributedCache _distributedCache;

        public OperationsCache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task SaveAsync(Guid id, string state, string code = null, string reason = null)
        {
            var operation = await GetAsync(id) ?? new OperationDto();
            operation.Id = id;
            operation.State = state;
            operation.Code = code ?? string.Empty;
            operation.Reason = reason ?? string.Empty;
            await _distributedCache.SetStringAsync(id.ToString(), JsonConvert.SerializeObject(operation));
        }

        public async Task<OperationDto> GetAsync(Guid id)
        {
            var json = await _distributedCache.GetStringAsync(id.ToString());
            return string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<OperationDto>(json);
        }
    }
}
