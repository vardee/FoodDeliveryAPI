using backendTask.AdditionalService;
using System.Text.Json.Serialization;

namespace backendTask.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum HouseType
    {
        [EnumTranscription("Void")]
        Void,
        [EnumTranscription("Владение")]
        Ownership,
        [EnumTranscription("Дом")]
        House,
        [EnumTranscription("Домовладение")]
        Homeownership,
        [EnumTranscription("Гараж")]
        Garage,
        [EnumTranscription("Здание")]
        Building,
        [EnumTranscription("Шахта")]
        Mine,
        [EnumTranscription("Строение")]
        Сonstruction,
        [EnumTranscription("Сооружение")]
        TheStructure,
        [EnumTranscription("Литера")]
        Litera,
        [EnumTranscription("Корпус")]
        Body,
        [EnumTranscription("Подвал")]
        Basement,
        [EnumTranscription("Котельная")]
        BoilerRoom,
        [EnumTranscription("Погреб")]
        Cellar,
        [EnumTranscription("Объект незавершенного строительства")]
        Anobjectofunfinishedconstruction,
    }
}
