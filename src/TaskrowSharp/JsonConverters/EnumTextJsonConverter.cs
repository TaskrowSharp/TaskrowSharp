using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskrowSharp.JsonConverters;

public class EnumTextJsonConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            if (string.IsNullOrWhiteSpace(stringValue))
                throw new JsonException($"Cannot convert empty string to {typeof(T).Name}");

            foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var descriptionAttribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (descriptionAttribute != null && descriptionAttribute.Description.Equals(stringValue, StringComparison.OrdinalIgnoreCase))
                {
                    return (T)field.GetValue(null)!;
                }
            }

            if (Enum.TryParse<T>(stringValue, true, out var enumValue))
                return enumValue;

            throw new JsonException($"Unable to convert \"{stringValue}\" to enum {typeof(T).Name}");
        }

        if (reader.TokenType == JsonTokenType.Number)
        {
            var numericValue = reader.GetInt32();
            return (T)Enum.ToObject(typeof(T), numericValue);
        }

        throw new JsonException($"Unexpected token type {reader.TokenType} when parsing enum {typeof(T).Name}");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        var field = typeof(T).GetField(value.ToString());
        var descriptionAttribute = field?.GetCustomAttribute<DescriptionAttribute>();

        if (descriptionAttribute != null)
        {
            writer.WriteStringValue(descriptionAttribute.Description);
        }
        else
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
