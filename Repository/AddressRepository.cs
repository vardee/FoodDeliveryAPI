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
        public AddressRepository(AddressDBContext db, EnumTranscription enumTranscription)
        {
            _db = db;
            _enumTranscription = enumTranscription;
        }

        public async Task<List<AddressSearchDTO>> addressSearchDTO(long parentObjectId, string query)
        {
            var addressObjects = new List<AddressSearchDTO>();
            var currentObjectsHierachy = _db.AsAdmHiearchies.Where(co => co.parentobjid == parentObjectId).DefaultIfEmpty().ToList();

            foreach (var currentObject in currentObjectsHierachy)
            {
                var currentObjectsAddr = _db.AsAddrObjs.FirstOrDefault(a => a.objectid == currentObject.objectid && a.isactual == 1);
                var currentObjectsHouses = _db.AsHouses.FirstOrDefault(a => a.objectid == currentObject.objectid && a.isactual == 1);

                if ((currentObjectsAddr != null && (query == null || currentObjectsAddr.name.Contains(query) || string.IsNullOrEmpty(query))) ||
                    (currentObjectsHouses != null && (query == null || currentObjectsHouses.housenum.Contains(query) || string.IsNullOrEmpty(query))))
                {
                    if (currentObjectsAddr != null)
                    {
                        string textOut = currentObjectsAddr.typename + " " + currentObjectsAddr.name;
                        var currentObjectLevel = currentObjectsAddr.level;

                        addressObjects.Add(new AddressSearchDTO
                        {
                            objectId = (long)currentObject.objectid,
                            objectGuid = currentObjectsAddr.objectguid,
                            text = textOut,
                            objectLevel = ((GarAddressLevel)int.Parse(currentObjectsAddr.level)).ToString(),
                            objectLevelText = _enumTranscription.GetTranscription((GarAddressLevel)Enum.Parse(typeof(GarAddressLevel), currentObjectLevel))
                        });
                    }
                    else if (currentObjectsHouses != null)
                    {
                        string textOut = currentObjectsHouses.housenum;
                        var currentObjectLevel = currentObjectsHouses.housetype;
                        var currentObjectLevelAsString = ((HouseType)currentObjectLevel).ToString();


                        addressObjects.Add(new AddressSearchDTO
                        {
                            objectId = (long)currentObject.objectid,
                            objectGuid = currentObjectsHouses.objectguid,
                            text = textOut,
                            objectLevel = ((HouseType)currentObjectsHouses.housetype).ToString(),
                            objectLevelText = _enumTranscription.GetTranscription((HouseType)Enum.ToObject(typeof(HouseType), currentObjectLevel))
                        });
                    }
                }
            }

            return addressObjects;
        }

    }
}
