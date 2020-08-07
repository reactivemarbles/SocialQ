using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CosmosDbRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SocialQ.Functions
{
    public class Stores
    {
        private ICosmosDbRepository<StoreDocument> _storeRepository;
        private List<StoreDocument> _data;

        public Stores(ICosmosDb cosmosDb)
        {
            _storeRepository = cosmosDb.Repository<StoreDocument>();
            _data = new StoreDocumentGenerator().Items;
        }

        [FunctionName("GetAllStores")]
        public async Task<IActionResult> GetAllStores(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "stores")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            var documents = await _storeRepository.FindAsync();
            return new OkObjectResult(documents);
        }

        [FunctionName("GetStore")]
        public async Task<IActionResult> GetStore(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "stores/{id}")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            if (!Guid.TryParse(req.Query["id"], out Guid id))
            {
                return new NotFoundResult();
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

            // foreach (var storeDocument in _data)
            // {
            //     await _storeRepository.AddAsync(storeDocument);
            // }

            return new OkResult();
        }
    }
}