using Gql.Models;
using HotChocolate.Types;

namespace Gql.Graphql.Types;

public class ChampionMasteryType : ObjectType<ChampionMastery>
{
    protected override void Configure(IObjectTypeDescriptor<ChampionMastery> descriptor)
    {
        descriptor.Description("Represents champion mastery information for a player.");

        descriptor.Field(t => t.Puuid).Description("Encrypted PUUID.");
        descriptor.Field(t => t.ChampionId).Description("Champion ID.");
        descriptor.Field(t => t.ChampionLevel).Description("Champion level.");
        descriptor.Field(t => t.ChampionPoints).Description("Total champion points.");
        descriptor.Field(t => t.LastPlayTime).Description("Last time this champion was played (Unix ms).");
        descriptor.Field(t => t.ChestGranted).Description("Whether a chest was granted for this champion.");
        descriptor.Field(t => t.TokensEarned).Description("Tokens earned for the current level.");
    }
}
