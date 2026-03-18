using CommandsService.Events;

namespace CommandsService.Services;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class EventConverter : JsonConverter
{
    private static readonly Dictionary<string, Type> EventTypes = new()
    {
        ["PlatformPublished"] = typeof(PlatformPublishedEvent)
    };
    
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
    
    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);

        // Example discriminator
        var eventType = jo["EventType"]?.Value<string>();

        return eventType switch
        {
            "PlatformPublished" => jo.ToObject<PlatformPublishedEvent>(serializer),
            _ => throw new JsonSerializationException($"Unknown event type: {eventType}")
        };

    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(object);
    }
}