using System.Globalization;
using System.Text.Json;

namespace NotesLiftel.Converters
{
    public class DataTimeConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
    {
        private const string DateFormat = "dd/MM/yyyy HH:mm";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //DateTime data = DateTime.Parse(reader.GetString(), null, DateTimeStyles.AssumeUniversal);
            return DateTime.ParseExact(reader.GetString(), DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormat));
        }
    }
}
