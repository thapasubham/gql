using Gql.Models;
using HotChocolate.Types;

namespace Gql.Graphql.Types;

public class SummonerType : ObjectType<Summoner>
{
    protected override void Configure(IObjectTypeDescriptor<Summoner> descriptor)
    {
        descriptor.Description("Represents a League of Legends summoner.");

        descriptor.Field(s => s.Id).Description("The encrypted summoner ID.");
        descriptor.Field(s => s.AccountId).Description("The encrypted account ID.");
        descriptor.Field(s => s.Puuid).Description("The encrypted PUUID.");
        descriptor.Field(s => s.Name).Description("The summoner name.");
        descriptor.Field(s => s.SummonerLevel).Description("The summoner level.");
        descriptor.Field(s => s.ProfileIconId).Description("The ID of the profile icon.");
    }
}
