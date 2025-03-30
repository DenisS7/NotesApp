using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Newtonsoft.Json;

namespace NotesApp.Converters
{
    class ColorJsonConverter : JsonConverter<Color>
    {
        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            string hex = (string)reader.Value;
            return (Color)ColorConverter.ConvertFromString(hex);
        }
    }
}
