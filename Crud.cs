using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

public class LambdaHandler
{
    private const string TableName = "ItemsTable";
    private readonly IAmazonDynamoDB _dynamoDbClient;

    public LambdaHandler()
    {
        _dynamoDbClient = new AmazonDynamoDBClient();
    }

    public async Task<APIGatewayProxyResponse> GetAllItems(APIGatewayProxyRequest request, ILambdaContext context)
    {
        Table itemsTable = Table.LoadTable(_dynamoDbClient, TableName);

        ScanFilter scanFilter = new ScanFilter();
        Search search = itemsTable.Scan(scanFilter);
        List<Document> items = await search.GetNextSetAsync();

        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = Newtonsoft.Json.JsonConvert.SerializeObject(items),
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
    }

    public async Task<APIGatewayProxyResponse> GetItemById(APIGatewayProxyRequest request, ILambdaContext context)
    {
        string itemId = request.PathParameters["id"];
        Table itemsTable = Table.LoadTable(_dynamoDbClient, TableName);

        Document item = await itemsTable.GetItemAsync(itemId);

        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = Newtonsoft.Json.JsonConvert.SerializeObject(item),
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
    }

    public async Task<APIGatewayProxyResponse> CreateItem(APIGatewayProxyRequest request, ILambdaContext context)
    {
        string requestBody = request.Body;
        Document newItem = Document.FromJson(requestBody);

        Table itemsTable = Table.LoadTable(_dynamoDbClient, TableName);
        await itemsTable.PutItemAsync(newItem);

        return new APIGatewayProxyResponse
        {
            StatusCode = 201,
            Body = "Item created successfully",
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };
    }

    public async Task<APIGatewayProxyResponse> UpdateItem(APIGatewayProxyRequest request, ILambdaContext context)
    {
        string itemId = request.PathParameters["id"];
        string requestBody = request.Body;
        Document updatedItem = Document.FromJson(requestBody);

        Table itemsTable = Table.LoadTable(_dynamoDbClient, TableName);
        await itemsTable.UpdateItemAsync(updatedItem, itemId);

        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = "Item updated successfully",
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };
    }

    public async Task<APIGatewayProxyResponse> DeleteItem(APIGatewayProxyRequest request, ILambdaContext context)
    {
        string itemId = request.PathParameters["id"];

        Table itemsTable = Table.LoadTable(_dynamoDbClient, TableName);
        await itemsTable.DeleteItemAsync(itemId);

        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = "Item deleted successfully",
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };
    }
}
