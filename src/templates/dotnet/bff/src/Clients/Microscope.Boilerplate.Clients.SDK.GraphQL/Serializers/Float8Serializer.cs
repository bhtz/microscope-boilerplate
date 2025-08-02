using StrawberryShake.Serialization;

namespace Microscope.Boilerplate.Clients.SDK.GraphQL.Serializers;

public class Float8Serializer : ScalarSerializer<Double>
{
    public Float8Serializer() : base("float8")
    {
    }
}