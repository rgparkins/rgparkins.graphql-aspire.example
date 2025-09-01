using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Register the GraphQL service and expose an HTTP endpoint
var graphqlApi = builder.AddProject<Projects.API>("graphqlapi").
    WithExternalHttpEndpoints();

builder.Build().Run();