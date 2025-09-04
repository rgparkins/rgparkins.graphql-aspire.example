using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace API.Tests.Common;

public class TestingContext : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    protected readonly HttpClient _client;
    protected HttpResponseMessage Response;

    public TestingContext(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    public async Task Search(int firstAmount, string sort = "{ publishedAt: DESC }", string searchTerm = "{ title: { contains: \"e\" } }")
    {
        var query = $@"query {{
          books(first: {firstAmount}, order: {sort}, where: {searchTerm}) {{
            totalCount
            nodes {{
              id title publishedAt
            }}
          }}
        }}";
        
        var content = new StringContent(
            JsonConvert.SerializeObject(new
            {
                query
            }),
            Encoding.UTF8,
            "application/json");
        
        
        Response = await _client.PostAsync("/", content);
    }

    public void Dispose()
    {
        _client.Dispose();
        Response.Dispose();
    }
}