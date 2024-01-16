using FunctionsDemo.Abstractions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace FunctionsDemo.Functions;

public class AddOrderFunction
{
    private readonly ILogger _logger;

    public AddOrderFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<AddOrderFunction>();
    }

    [Function("AddOrderFunction")]
    public MultiOutput Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "add-order")] HttpRequestData req, [FromBody] Order order)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString("Welcome to Azure Functions!");

        return new MultiOutput()
        {
            order = new Order 
            {
                Id = Guid.NewGuid().ToString(),
                Email = Constants.ReceiverEmail,
                StoreId = Constants.StoreId,
                ProductName = order.ProductName,
                Price = order.Price,
                Quantity = order.Quantity
            },
            HttpResponse = response
        };

    }
}

public record MultiOutput
{
    [CosmosDBOutput(
        databaseName: Constants.CosmosDBDatabaseName,
        containerName: Constants.CosmosDBContainerName,
        Connection = Constants.CosmosDBConnectionName)]
    public required Order order { get; set; }

    public required HttpResponseData HttpResponse { get; set; }
}