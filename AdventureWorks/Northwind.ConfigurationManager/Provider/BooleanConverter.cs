using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Northwind.ConfigurationManager.Provider
{
    public class BooleanConverter : JsonConverter<bool>
    {
        public override bool Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
            Boolean.Parse(reader.GetString());

        public override void Write(
            Utf8JsonWriter writer,
            bool value,
            JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString());
    }
}
