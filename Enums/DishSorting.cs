namespace backendTask.Enums;
using System.Text.Json.Serialization;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DishSorting
{
    NameAsc, 
    NameDesc, 
    PriceAsc, 
    PriceDesc, 
    RatingAsc, 
    RatingDesc
}

