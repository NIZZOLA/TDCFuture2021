using LocationMinApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationMinApi.Interfaces
{
    interface ILocationRepository
    {
        Task<ICollection<LocationModel>> GetAll();
        Task<LocationModel> GetOne(int id);
        Task<ICollection<LocationModel>> GetFromGeo(double latitude, double longitude);
        Task<LocationModel> Add(LocationModel model);
        Task<bool> Delete(int id);
    }
}
