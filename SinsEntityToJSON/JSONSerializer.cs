using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SinsEntityConverter
{
    public class EntityBoolConverter : JsonConverter<EntityBool?>
    {
        public override void Write(
            Utf8JsonWriter writer,
            EntityBool? value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Value is null but IsEmpty is true, serialize as Value: null, IsEmpty: true
            if (value!.Value.Value == null &&
                value.Value.IsEmpty == true) // Unwrap the nullable struct first
            {
                writer.WriteNull("Value");
                writer.WriteBoolean("IsEmpty", true);
            }
            else // Otherwise Value is not null
            {
                writer.WriteBoolean("Value", (bool)value.Value.Value!);
            }

            writer.WriteEndObject();
        }

        public override EntityBool? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            return new EntityBool();
        }
    }

    public class EntityIntConverter : JsonConverter<EntityInt?>
    {
        public override void Write(
            Utf8JsonWriter writer,
            EntityInt? value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Value is null but IsEmpty is true, serialize as Value: null, IsEmpty: true
            if (value!.Value.Value == null &&
                value.Value.IsEmpty == true) // Unwrap the nullable struct first
            {
                writer.WriteNull("Value");
                writer.WriteBoolean("IsEmpty", true);
            }
            else // Otherwise Value is not null
            {
                writer.WriteNumber("Value", (int)value.Value.Value!);
            }

            writer.WriteEndObject();
        }

        public override EntityInt? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            return new EntityInt();
        }
    }

    public class EntityIntKeyConverter : JsonConverter<EntityInt>
    {
        public override void Write(
            Utf8JsonWriter writer,
            EntityInt value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Value is null but IsEmpty is true, serialize as Value: null, IsEmpty: true
            if (value.Value == null && 
                value.IsEmpty == true)
            {
                writer.WriteNull("Value");
                writer.WriteBoolean("IsEmpty", true);
            }
            else
            {
                writer.WriteNumber("Value", (int)value.Value!);
            }

            writer.WriteEndObject();
        }
        public override EntityInt Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new EntityInt();
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            [DisallowNull] EntityInt value,
            JsonSerializerOptions options)
        {
            writer.WritePropertyName(
                value.Value!.Value.ToString(CultureInfo.InvariantCulture));
        }

        public override EntityInt ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            return new EntityInt(reader.GetString()!);
        }
    }

    public class EntityFloatConverter : JsonConverter<EntityFloat?>
    {
        public override void Write(
            Utf8JsonWriter writer,
            EntityFloat? value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Value is null but IsEmpty is true, serialize as Value: null, IsEmpty: true
            if (value!.Value.Value == null &&
                value.Value.IsEmpty == true) // Unwrap the nullable struct first
            {
                writer.WriteNull("Value");
                writer.WriteBoolean("IsEmpty", true);
            }
            else // Otherwise Value is not null
            {
                writer.WriteNumber("Value", (float)value.Value.Value!);
            }

            writer.WriteEndObject();
        }

        public override EntityFloat? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            return new EntityFloat();
        }
    }

    public class JSONSerializer
    {
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        static JSONSerializer()
        {
            options.Converters.Add(new EntityBoolConverter());
            options.Converters.Add(new EntityIntConverter());
            options.Converters.Add(new EntityIntKeyConverter());
            options.Converters.Add(new EntityFloatConverter());
        }

        public static void Serialize(Def def, 
            SinsFile file, 
            Action<RenderCode, SinsFile?, string> render,
            DataRepresentations data)
        {
            string json = JsonSerializer.Serialize(def, options);

            string outputPath = Path.ChangeExtension(file.Path, ".json")!;
            File.WriteAllText(outputPath, json);

            if (!File.Exists(outputPath))
            {
                file.OperationCode = OperationCode.ConversionFailed;
                render(RenderCode.ConversionFailed, file, "");
                data.curFiles!.Remove(file);
                return;
            }

            string newPath = Path.Combine(Deserializer.OutputDir!, Path.GetFileName(outputPath));

            File.Move(outputPath, newPath);
            file.OperationCode = OperationCode.Success;
            render(RenderCode.ConversionSuccess, file, newPath);
        }
    }
}
