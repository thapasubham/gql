var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IPlayerStore, PlayerStore>();
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();
builder.WebHost.UseUrls("http://localhost:5000");
var app = builder.Build();

app.MapGraphQL("/graphql");
app.MapGet("/", () => "Hello World!");
app.Lifetime.ApplicationStarted.Register(() =>
{
    Console.WriteLine("GraphQL Server running at:");
    foreach (var url in app.Urls)
    {
        Console.WriteLine($"{url}");
    }
});

app.Run();
