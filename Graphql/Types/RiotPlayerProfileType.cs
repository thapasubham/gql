using Gql.Models;
using HotChocolate.Types;

namespace Gql.Graphql.Types;

public class RiotPlayerProfileType : ObjectType<RiotPlayerProfile>
{
    protected override void Configure(IObjectTypeDescriptor<RiotPlayerProfile> descriptor)
    {
        descriptor.Description("League of Legends player profile from the Riot API.");

        descriptor.Field(p => p.Summoner).Description("Summoner account info.");
        descriptor.Field(p => p.ChampionMasteries).Description("Champion mastery records for this player.");
        descriptor.Field(p => p.RecentMatches).Description("Recent match IDs for this player.");
    }
}
