using System.Text.Json;
using System.Text.Json.Serialization;

namespace BanishedDev.Libs.Data.Core;

public class DataCollectionJsonConverter<T> : JsonConverter<DataCollection<T>> where T : unmanaged
{
    public override DataCollection<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var collection = new DataCollection<T>(5);

        while(reader.Read())
        {
            if(reader.TokenType != JsonTokenType.EndArray) {
                var obj = JsonSerializer.Deserialize<T>(ref reader);
                collection.Add(obj);
            }
        }

        return collection;
    }

    public override void Write(Utf8JsonWriter writer, DataCollection<T> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        for(int x = 0; x < value.Count; x++)
        {
            JsonSerializer.Serialize<T>(writer, value[x]);
        }
        writer.WriteEndArray();
    }
}