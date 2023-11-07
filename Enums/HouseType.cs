using backendTask.AdditionalService;

namespace backendTask.Enums
{
    public enum HouseType
    {
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
