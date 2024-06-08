

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;

public static class MyJsonSerialize
{

    public static string Serialize<T>(T obj)
    {
        var options = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };

        return JsonSerializer.Serialize<T>(obj, options);

    }

    public static T Deserialize<T>(string jsonData)
    {
        var options = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
        };

        T obj = JsonSerializer.Deserialize<T>(jsonData, options);

        return obj;
    }
}