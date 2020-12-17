using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CosmosDbRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using SocialQ.Functions.Store;

namespace SocialQ.Functions.Queue
{
    public class Queue
    {
        private readonly ICosmosDbRepository<QueueDocument> _queueRepository;

        public Queue(ICosmosDb cosmosDb) => _queueRepository = cosmosDb.Repository<QueueDocument>();

        [FunctionName("UpdateQueue")]
        public async Task UpdateQueue(
            [CosmosDBTrigger("databaseName", "collectionName", ConnectionStringSetting = "", LeaseCollectionName = "leases")] IReadOnlyList<Document> input,
            [SignalR(HubName = "chat")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);

                // Rerun Stored Procedure

                // Send back new queue to user
                await signalRMessages.AddAsync(new SignalRMessage()).ConfigureAwait(false);
            }
        }

        [FunctionName("AddStore")]
        public async Task<IActionResult> AddStore(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "queues/")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger {nameof(AddStore)} function processed a request.");

            var dto = await req.Convert<QueueDocument>().ConfigureAwait(false);

            if (dto == null)
            {
                return new BadRequestObjectResult("The dto provided is not valid.");
            }

            var documents = await _queueRepository.AddAsync(dto).ConfigureAwait(false);
            return new OkObjectResult(documents);
        }
    }
}