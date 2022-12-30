using System.Text.Json;
using System.Text.Json.Serialization;

namespace BanishedDev.Libs.Data.Core;

public class DataCollectionJsonFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
         return false;

      var type = typeToConvert;
      var keyType = type.GenericTypeArguments[0];

      if(!keyType.IsValueType) return false;
        
      if (!type.IsGenericTypeDefinition)
         type = type.GetGenericTypeDefinition();

      return type == typeof(DataCollection<>);
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var keyType = typeToConvert.GenericTypeArguments[0];
        var converterType = typeof(DataCollectionJsonConverter<>).MakeGenericType(keyType);

        return (JsonConverter?) Activator.CreateInstance(converterType);
    }
}