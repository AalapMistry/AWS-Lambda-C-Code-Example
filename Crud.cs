using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace YourNamespace
{
    public class Functions
    {
        private readonly IRepository _repository;

        public Functions()
        {
            _repository = new Repository(); // Your repository implementation
        }

        public APIGatewayProxyResponse Get(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var id = request.PathParameters["id"];
            var item = _repository.Get(id);
            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)(item == null ? HttpStatusCode.NotFound : HttpStatusCode.OK),
                Body = JsonConvert.SerializeObject(item),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
            return response;
        }

        // Implement other CRUD operations (Create, Update, Delete) similarly
    }
}
