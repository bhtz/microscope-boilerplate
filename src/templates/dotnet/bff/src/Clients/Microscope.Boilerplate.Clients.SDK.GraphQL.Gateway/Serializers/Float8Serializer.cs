using StrawberryShake.Serialization;

namespace Microscope.Boilerplate.Clients.SDK.GraphQL.Gateway.Serializers;

public class Float8Serializer : ScalarSerializer<Double>
{
    public Float8Serializer() : base("float8")
    {
    }
}