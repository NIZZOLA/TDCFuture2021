using Dapper;
using LocationMinApi.Contracts;
using LocationMinApi.Data;
using LocationMinApi.Interfaces;
using LocationMinApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LocationMinApi.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly AppDbContext _context;
        private string _connectionString;

        public PlaceRepository(AppDbContext context)
        {
            _context = context;
            _connectionString = _context.Database.GetDbConnection().ConnectionString;
        }

        public async Task<PlaceModel> Add(PlaceModel model)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var procedure = "PlaceInsert";
                var values = new
                {
                    @Lat = model.Latitude,
                    @Long = model.Longitude,
                    @Name = model.Name,
                    @Address = model.Address,
                    @Description = model.Description,
                    @PhoneNumber = model.PhoneNumber,
                    @Snippet = model.Snippet,
                    @Id = model.Id
                };

                var result = await db.QueryFirstAsync<PlaceModel>(procedure, values, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var placeModel = await _context.Places.FindAsync(id);
            if (placeModel == null)
            {
                return false;
            }

            _context.Places.Remove(placeModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<PlaceModel>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var result = await db.QueryAsync<PlaceModel>("select * from Places");
                return result.ToList();
            }
        }

        public async Task<ICollection<PlaceModel>> GetByLocation(PlacesByGeoRequest requestParam)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var procedure = "GetPlacesOn";
                var values = new
                {
                    @Lat = requestParam.Latitude,
                    @Long = requestParam.Longitude,
                    @Ray = requestParam.Meters,
                    @PlaceType = requestParam.Type == null ? "" : requestParam.Type
                };

                var result = await db.QueryAsync<PlaceModel>(procedure, values, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<ICollection<PlaceModel>> GetByLocationId(int locationId, string type)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var procedure = "GetPlacesByLocationId";
                var values = new
                {
                    @LocationId = locationId,
                    @PlaceType = type == null ? "" : type
                };

                var result = await db.QueryAsync<PlaceModel>(procedure, values, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<PlaceModel> GetOne(int id)
        {
            return await _context.Places.Where(e => e.PlaceId == id).FirstOrDefaultAsync();
        }
    }
}
