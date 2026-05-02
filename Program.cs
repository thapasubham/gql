using Gql.Auth;
using Gql.Client;
using Gql.Graphql;
using Gql.Graphql.Queries;
using Gql.Graphql.Types;
using Gql.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGqlMongoDb(builder.Configuration);
builder.Services.AddGqlJwtAuthentication(builder.Configuration);
builder.Services.AddSingleton<JwtTokenService>();
builder.Services.AddSingleton<IPlayerStore, MongoPlayerStore>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IRiotApiService, RiotApiService>();
builder.Services.AddHostedService<PlayerCollectionSeeder>();
builder.Services.AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType<Query>()
    .AddTypeExtension<SummonerQuery>()
    .AddType<SummonerType>()
    .AddType<ChampionMasteryType>()
    .AddMutationType<Mutation>();
builder.WebHost.UseUrls("http://localhost:5000");
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost(
        "/auth/login",
        async Task<IResult> (
            [FromBody] LoginRequest request,
            IPlayerStore store,
            JwtTokenService tokens,
            IOptions<JwtOptions> jwtOptions,
            CancellationToken cancellationToken) =>
        {
            var jwt = jwtOptions.Value;
            if (string.IsNullOrEmpty(jwt.LoginPassword))
                return Results.Json(new { error = "Login is not configured." }, statusCode: StatusCodes.Status503ServiceUnavailable);

            if (string.IsNullOrWhiteSpace(request.Username)
                || request.Password is null
                || request.Password != jwt.LoginPassword)
                return Results.Unauthorized();

            var player = await store.GetByUsernameAsync(request.Username.Trim(), cancellationToken);
            if (player is null)
                return Results.Unauthorized();

            var accessToken = tokens.CreateAccessToken(player.Username);
            return Results.Json(new
            {
                access_token = accessToken,
                token_type = "Bearer",
                expires_in = jwt.ExpiryMinutes * 60
            });
        })
    .WithTags("Auth");

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
