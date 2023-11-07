﻿using backendTask.AdditionalService;
using System;
namespace backendTask.Enums
{
    public enum GarAdressLevel
    {
        [EnumTranscription("Регион")]
        Region,
        [EnumTranscription("Административный район ")]
        AdministrativeArea,
        [EnumTranscription("Муниципальный район ")]
        MunicipalArea,
        [EnumTranscription("Сельское поселение")]
        RuralUrbanSettlement,
        [EnumTranscription("Город")]
        City,
        [EnumTranscription("Район")]
        Locality,
        [EnumTranscription("Элемент планировочной структуры")]
        ElementOfPlanningStructure,
        [EnumTranscription("Элемент глобальной сети")]
        ElementOfRoadNetwork,
        [EnumTranscription("Край")]
        Land,
        [EnumTranscription("Здание")]
        Building,
        [EnumTranscription("Комната")]
        Room,
        [EnumTranscription("Жилые комнаты")]
        RoomInRooms,
        [EnumTranscription("Автономный региональный уровень")]
        AutonomousRegionLevel,
        [EnumTranscription("Внутригородской уровень")]
        IntracityLevel,
        [EnumTranscription("Уровень дополнительных территорий")]
        AdditionalTerritoriesLevel,
        [EnumTranscription("Уровень объектов в дополнительных территориях")]
        LevelOfObjectsInAdditionalTerritories,
        [EnumTranscription("Парковка")]
        CarPlace
    }
}
