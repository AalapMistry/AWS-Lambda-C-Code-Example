using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new AmazonDynamoDBClient();

        var tableName = "YourTableName";
        var table = Table.LoadTable(client, tableName);

        // Create operation
        await CreateItemAsync(table, "1", "John", 30);

        // Read operation
        await ReadItemAsync(table, "1");

        // Update operation
        await UpdateItemAsync(table, "1", "Jane");

        // Read updated item
        await ReadItemAsync(table, "1");

        // Delete operation
        await DeleteItemAsync(table, "1");
    }

    static async Task CreateItemAsync(Table table, string id, string name, int age)
    {
        var document = new Document();
        document["Id"] = id;
        document["Name"] = name;
        document["Age"] = age;

        await table.PutItemAsync(document);
        Console.WriteLine($"Item with Id {id} created successfully.");
    }

    static async Task ReadItemAsync(Table table, string id)
    {
        var document = await table.GetItemAsync(id);
        if (document != null)
        {
            Console.WriteLine($"Item found - Id: {document["Id"]}, Name: {document["Name"]}, Age: {document["Age"]}");
        }
        else
        {
            Console.WriteLine($"Item with Id {id} not found.");
        }
    }

    static async Task UpdateItemAsync(Table table, string id, string newName)
    {
        var updateExpression = new UpdateItemOperationConfig
        {
            AttributeUpdates = new AttributeUpdates
            {
                ["Name"] = new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = newName } }
            }
        };

        await table.UpdateItemAsync(id, updateExpression);
        Console.WriteLine($"Item with Id {id} updated successfully.");
    }

    static async Task DeleteItemAsync(Table table, string id)
    {
        await table.DeleteItemAsync(id);
        Console.WriteLine($"Item with Id {id} deleted successfully.");
    }
}
