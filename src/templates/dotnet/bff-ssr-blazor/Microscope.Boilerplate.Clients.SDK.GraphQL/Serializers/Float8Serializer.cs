using StrawberryShake.Serialization;

namespace Mylight.SmartDigitalCoffee.SDK.GraphQL.Serializers;

public class Float8Serializer : ScalarSerializer<Double>
{
    public Float8Serializer() : base("float8")
    {
    }
}