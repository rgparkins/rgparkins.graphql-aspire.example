namespace API.Types;
public class Query
{
    public string Hello() => "Hello from Aspire + GraphQL 👋";
    public Weather TodayWeather() => new("London", 21, "Partly Cloudy");
}
public record Weather(string City, int TempC, string Summary);