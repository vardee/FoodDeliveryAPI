using backendTask.AdditionalService;
using backendTask.DataBase;
using backendTask.DataBase.Models;
using backendTask.DBContext;
using backendTask.DBContext.Dto.AddressDTO;
using backendTask.Enums;
using backendTask.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mime;
using System.Numerics;

namespace backendTask.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AddressDBContext _db;
        private readonly EnumTranscription _enumTranscription;
        public AddressRepository(AddressDBContext db,EnumTranscription enumTranscription)
        {
            _db = db;
            _enumTranscription = enumTranscription;
        }
        public async Task<List<AddressSearchDTO>> addressSearchDTO(long parentObjectId, string query)
        {
            var addressObjects = new List<AddressSearchDTO>();
            var childrenDTO = from currentObject in _db.as_adm_hierachy.DefaultIfEmpty()
                              where currentObject.parentobjid == parentObjectId
                              let currentObjectsAddr = _db.as_addr_obj.FirstOrDefault(a => a.objectid == currentObject.objectid && a.isactual == 1)
                              let currentObjectsHouses = _db.as_houses.FirstOrDefault(a => a.objectid == currentObject.objectid && a.isactual == 1)
                              select new
                              {
                                  currentObject,
                                  currentObjectsAddr,
                                  currentObjectsHouses
                              };

            foreach (var child in childrenDTO)
            {
                var currentObject = child.currentObject;
                var currentObjectsAddr = child.currentObjectsAddr;
                var currentObjectsHouses = child.currentObjectsHouses;

                if (currentObjectsAddr != null && (currentObjectsAddr.name.Contains(query) || string.IsNullOrEmpty(query)))
                {
                    string textOut = currentObjectsAddr.typename + " " + currentObjectsAddr.name;
                    var currentObjectLevel = currentObjectsAddr.level;

                    var dto = new AddressSearchDTO
                    {
                        objectId = currentObject.id,
                        objectGuid = currentObjectsAddr.objectguid,
                        text = textOut,
                        objectLevel = (GarAdressLevel)Enum.Parse(typeof(GarAdressLevel), currentObjectLevel),
                        objectLevelText = _enumTranscription.GetTranscription((HouseType)Enum.Parse(typeof(HouseType), currentObjectLevel))
                    };

                    addressObjects.Add(dto);
                }
                else if (currentObjectsHouses != null && (currentObjectsHouses.housenum.Contains(query) || string.IsNullOrEmpty(query)))
                {
                    string textOut = currentObjectsHouses.housenum;
                    var currentObjectLevel = currentObjectsHouses.housetype;

                    var dto = new AddressSearchDTO
                    {
                        objectId = currentObject.id,
                        objectGuid = currentObjectsHouses.objectguid,
                        text = textOut,
                        objectLevel = (HouseType)currentObjectLevel,
                        objectLevelText = _enumTranscription.GetTranscription((HouseType)currentObjectLevel)
                    };

                    addressObjects.Add(dto);
                }
            }

            return addressObjects;
        }

    }
}
