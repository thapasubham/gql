using Gql.Models;
using HotChocolate.Types;

namespace Gql.Graphql.Types;

public class MatchType : ObjectType<Match>
{
    protected override void Configure(IObjectTypeDescriptor<Match> descriptor)
    {
        descriptor.Description("A League of Legends match reference.");

        descriptor.Field(m => m.Id).Description("The match ID.");
    }
}
