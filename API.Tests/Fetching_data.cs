using System.Text;
using API.Tests.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace API.Tests;

public class Fetching_data : TestingContext
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Fetching_data(ITestOutputHelper testOutputHelper) : base(new WebApplicationFactory<Program>())
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Get_Endpoint_ReturnsSuccess()
    {
        await Search(5);

        Response.EnsureSuccessStatusCode(); // Checks for 2xx status code

        var jObject = JsonConvert.DeserializeObject<JObject>(await Response.Content.ReadAsStringAsync());

        // Check for GraphQL errors
        Assert.False(jObject.TryGetValue("errors", out _), "GraphQL response contains errors");

        // Validate data
        Assert.True(jObject.TryGetValue("data", out var dataNode));
        Assert.True(((JObject)dataNode).TryGetValue("books", out var books));

        Assert.Equal(3, (int)books["totalCount"]);
    }
}