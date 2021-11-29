using Dapper;
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

namespace LocationMinApi.Repository
{
    public class LocationRepository : ILocationRepository
    {
        public readonly AppDbContext _context;
        private string _connectionString;

        public LocationRepository(AppDbContext context)
        {
            _context = context;
            _connectionString = _context.Database.GetDbConnection().ConnectionString;
        }
        public async Task<LocationModel> Add(LocationModel model)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var procedure = "LocationInsert";
                var values = new
                {
                    @Lat = model.Latitude,
                    @Long = model.Longitude,
                    @GeoMultiPoly = model.PolygonString,
                    @PlaceName = model.LocationName,
                    @PlaceCode = model.LocationCode,
                    @PlaceType = (int)model.LocationType
                };

                var results = await db.QueryAsync<LocationModel>(procedure, values, commandType: CommandType.StoredProcedure);
                return results.FirstOrDefault();
            }
        }

        public async Task<ICollection<LocationModel>> GetFromGeo(double latitude, double longitude)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var procedure = "WhatLocationIs";
                var values = new { @Lat = latitude, @Long = longitude };
                var results = await connection.QueryAsync<LocationModel>(procedure, values, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
        }

        public async Task<ICollection<LocationModel>> GetAll()
        {
            var result = await _context.Locations.ToListAsync();
            return result;
        }

        public async Task<LocationModel> GetOne(int id)
        {
            return await _context.Locations.Where(a => a.LocationId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var locationModel = await _context.Locations.FindAsync(id);
            if (locationModel == null)
            {
                return false;
            }

            _context.Locations.Remove(locationModel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
