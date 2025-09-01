using API.Data;
using API.DataLoaders;
using API.Services;
using API.Types;

var builder = WebApplication.CreateBuilder(args);

// (Optional) if you have Aspire ServiceDefaults:
// builder.Services.AddServiceDefaults();

builder.Services.AddSingleton<InMemoryStore>();
builder.Services.AddSingleton<BookService>();
builder.Services.AddDataLoader<AuthorByIdDataLoader>();

// GraphQL server
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<BookResolvers>()           // attach Book -> Author resolver
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions();

var app = builder.Build();

// (Optional) Aspire defaults:
// app.UseServiceDefaults();

app.UseWebSockets();           // required for subscriptions
app.MapGraphQL("/graphql");    // Banana Cake Pop UI at this path

// Seed data once per run
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<InMemoryStore>();
    Seed.EnsureSeeded(db);
}

app.Run();