using System.Text.Json;
using System.Text.Json.Serialization;

namespace ItemSystem.JsonConverters;

internal class ItemPropertyAttributeJsonConverter : JsonConverter<ItemPropertyAttribute>
{
    public override ItemPropertyAttribute? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        var attributeTypeName = "";
        var itemPropertyName = "";
        var defaultValue = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return new ItemPropertyAttribute(attributeTypeName, itemPropertyName, defaultValue);
            }

            if (reader.TokenType != JsonTokenType.PropertyName) { throw new JsonException(); }

            var propertyName = reader.GetString();
            reader.Read();
            switch (propertyName)
            {
                case nameof(ItemPropertyAttribute.AttributeType):
                    attributeTypeName = reader.GetString();
                    break;
                case nameof(ItemPropertyAttribute.ItemProperty):
                    itemPropertyName = reader.GetString();
                    break;
                case nameof(ItemPropertyAttribute.DefaultValue):
                    defaultValue = reader.GetInt32();
                    break;
            }
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, ItemPropertyAttribute value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName(GetPropertyName(nameof(value.AttributeType), options));
        writer.WriteStringValue(value.AttributeType.Name);

        writer.WritePropertyName(GetPropertyName(nameof(value.ItemProperty), options));
        writer.WriteStringValue(value.ItemProperty.Name);

        writer.WritePropertyName(GetPropertyName(nameof(value.DefaultValue), options));
        writer.WriteNumberValue(value.DefaultValue);

        writer.WriteEndObject();
    }
    private string GetPropertyName(string propertyName, JsonSerializerOptions options)
    {
        return options.PropertyNamingPolicy?.ConvertName(propertyName) ?? propertyName;
    }
}