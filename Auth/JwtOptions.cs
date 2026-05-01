namespace Gql.Auth;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    /// <summary>JWT issuer (iss claim).</summary>
    public string Issuer { get; set; } = "gql";

    /// <summary>JWT audience (aud claim).</summary>
    public string Audience { get; set; } = "gql-api";

    /// <summary>Symmetric signing key; must be at least 32 bytes for HS256.</summary>
    public string SigningKey { get; set; } = "";

    public int ExpiryMinutes { get; set; } = 60;

    /// <summary>
    /// Shared password required in login requests to obtain a token.
    /// Pair with an existing player username (see seed data).
    /// </summary>
    public string LoginPassword { get; set; } = "";
}
