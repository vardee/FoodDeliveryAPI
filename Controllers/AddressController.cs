using backendTask.DataBase.Dto.OrderDTO;
using backendTask.DataBase;
using backendTask.DBContext.Models;
using backendTask.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using backendTask.DBContext;
using System.Numerics;
using backendTask.DBContext.Dto.AddressDTO;

namespace backendTask.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressRepository _addressRepo;
        private readonly AddressDBContext _db;

        public AddressController(IAddressRepository addressRepo, AddressDBContext db)
        {
            _addressRepo = addressRepo;
            _db = db;
        }

        [HttpGet("address")]
        [ProducesResponseType(typeof(AddressSearchDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> addressSearch(long parentObjectId, string query)
        {
            return Ok(await _addressRepo.addressSearchDTO(parentObjectId, query));
        }
    }
}
