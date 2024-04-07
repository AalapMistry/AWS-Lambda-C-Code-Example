markdown
Copy code
# CRUD API using AWS Lambda and API Gateway

This project demonstrates how to create a CRUD (Create, Read, Update, Delete) API using AWS Lambda functions with C#.

## Setup

1. Clone this repository.
2. Open the solution in Visual Studio.
3. Modify the `Repository` class to interact with your data source.
4. Deploy the Lambda function to AWS Lambda.
5. Create an API Gateway and configure it to trigger the Lambda function.
6. Test the API using tools like Postman or curl.

## Endpoints

- GET /items/{id}: Retrieve an item by ID.
- POST /items: Create a new item.
- PUT /items/{id}: Update an existing item.
- DELETE /items/{id}: Delete an item by ID.

## Example Requests

### GET /items/1

```json
{
    "id": "1",
    "name": "Item 1"
}
