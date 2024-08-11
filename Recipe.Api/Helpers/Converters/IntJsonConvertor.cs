using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Recipe.Api.Helpers.Converters
{
    public class IntJsonConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (int.TryParse(reader.GetString(), out var intValue))
                {
                    return intValue;
                }
                else
                {
                    throw new JsonException("Value must be a valid integer.");
                }
            }

            if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var value))
            {
                return value;
            }

            throw new JsonException("Value must be a valid integer.");
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
