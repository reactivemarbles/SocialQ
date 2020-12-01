using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosDbRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace SocialQ.Functions.Store
{
    public class Stores
    {
        private readonly ICosmosDbRepository<StoreDocument> _storeRepository;

        public Stores(ICosmosDb cosmosDb) => _storeRepository = cosmosDb.Repository<StoreDocument>();

        [FunctionName("GetAllStores")]
        public async Task<IActionResult> GetAllStores(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "stores")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation($"C# HTTP trigger {nameof(GetAllStores)} function processed a request.");

            var documents = await _storeRepository.FindAsync();
            return new OkObjectResult(documents);
        }

        [FunctionName("GetStore")]
        public async Task<IActionResult> GetStore(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "stores/{id}")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation($"C# HTTP trigger {nameof(GetStore)} function processed a request.");

            if (!Guid.TryParse(req.Query["id"], out var id))
            {
                return new BadRequestObjectResult($"{nameof(id)} not provided");
            }
            
            var documents = await _storeRepository.FindAsync(x => x.Id == id);
            return new OkObjectResult(documents);
        }

        [FunctionName(nameof(LoadStores))]
        public async Task<IActionResult> LoadStores(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "stores/")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // var data = new StoreDocumentGenerator().Items;

            // foreach (var storeDocument in data)
            // {
            //     await _storeRepository.AddAsync(storeDocument);
            // }

            return new OkResult();
        }

        [FunctionName("GetStoreMetadata")]
        public async Task<IActionResult> GetStoreMetadata(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "metadata/stores")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger {nameof(GetStoreMetadata)} function processed a request.");

            var documents = await _storeRepository.SelectAsync(x => x.Name);
            var grouping = documents.GroupBy(x => x).SelectMany(x => x.Distinct());
            return new OkObjectResult(grouping);
        }
    }
}