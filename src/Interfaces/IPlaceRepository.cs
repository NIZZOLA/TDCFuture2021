using LocationMinApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationMinApi.Interfaces
{
    interface IPlaceRepository
    {
        Task<ICollection<PlaceModel>> GetAll();
        Task<PlaceModel> GetOne(int id);
        Task<ICollection<PlaceModel>> GetByLocation(PlacesByGeoRequest requestParam);
        Task<ICollection<PlaceModel>> GetByLocationId(int locationId, string type);
        Task<PlaceModel> Add(PlaceModel model);
        Task<bool> Delete(int id);
    }
}
