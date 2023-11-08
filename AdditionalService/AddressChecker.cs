using backendTask.DBContext;
using Microsoft.EntityFrameworkCore;

namespace backendTask.AdditionalService
{
    public class AddressChecker
    {
        public async static Task<bool> IsAddressNormal(AddressDBContext _adb, Guid? addressId)
        {
            var addressNormal = (await _adb.AsAddrObjs.FirstOrDefaultAsync(ab => ab.objectguid == addressId && ab.isactual == 1) != null) ||
                               (await _adb.AsHouses.FirstOrDefaultAsync(ah => ah.objectguid == addressId && ah.isactual == 1) != null);
            return addressNormal;
        }
    }
}
