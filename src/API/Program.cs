using API.Types;
using HotChocolate.AspNetCore.Voyager;

var builder = WebApplication.CreateBuilder(args);

// Observability/health from ServiceDefaults
builder.Services.AddServiceDefaults();

// GraphQL schema
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();
app.UseRouting();
app.UseServiceDefaults();

// GraphQL endpoint + Banana Cake Pop UI at /graphql
app.MapGraphQL("/graphql");

// Optional: Voyager schema explorer at /voyager
app.UseVoyager("/graphql", "/voyager");

app.Run();