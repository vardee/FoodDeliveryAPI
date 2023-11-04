namespace backendTask.Enums;
using System.Text.Json.Serialization;
[JsonConverter(typeof(JsonStringEnumConverter))]

public enum OrderStatus
{
   InProcess, 
   Delivered
}

