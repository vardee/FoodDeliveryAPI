using backendTask.Enums;
using System;

namespace backendTask.AdditionalService
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class EnumTranscription : Attribute
    {
        public string Value { get; }

        public EnumTranscription(string value)
        {
            Value = value;
        }
        public string GetTranscription(Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var transcriptionAttribute = (EnumTranscription)Attribute.GetCustomAttribute(fieldInfo, typeof(EnumTranscription));

            return transcriptionAttribute?.Value;
        }
    }
}
