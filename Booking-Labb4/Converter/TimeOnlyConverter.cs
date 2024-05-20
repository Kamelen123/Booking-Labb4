using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Booking_Labb4.Converter
{
    public class TimeOnlyConverter : JsonConverter<TimeOnly>
    {
        
        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                int hour = root.GetProperty("hour").GetInt32();
                int minute = root.GetProperty("minute").GetInt32();
                return new TimeOnly(hour, minute);
            }
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("hour", value.Hour);
            writer.WriteNumber("minute", value.Minute);
            writer.WriteEndObject();
        }
    }
}
