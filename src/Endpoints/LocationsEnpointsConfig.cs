using LocationMinApi.Contracts;
using LocationMinApi.Interfaces;
using LocationMinApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace LocationMinApi.Endpoints
{
    public class LocationEnpointsConfig
    {
        public static void AddEndpoints(WebApplication app)
        {
            app.MapGet("api/locations", async (ILocationRepository _locationRepository) =>
            {
                var result = await _locationRepository.GetAll();
                if (result == null)
                    return Results.NotFound();

                return Results.Ok(result);
            });

            app.MapGet("api/locations/{id}", async (ILocationRepository _locationRepository, int? id) =>
            {
                if (!id.HasValue)
                    return Results.BadRequest();

                var result = await _locationRepository.GetOne(id.Value);
                if (result == null)
                    return Results.NotFound();

                return Results.Ok(result);
            });

            app.MapGet("api/locations/geo/{latitude}/{longitude}", async (ILocationRepository _locationRepository, double? latitude, double? longitude) =>
            {
                if (!latitude.HasValue || !longitude.HasValue)
                    return Results.BadRequest();

                var hierarchy = await _locationRepository.GetFromGeo(latitude.Value, longitude.Value);

                return Results.Ok(hierarchy);
            });

            app.MapPost("api/locations", async (ILocationRepository _locationRepository, LocationModel location) =>
            {
                if (location == null)
                    return Results.BadRequest();

                var result = await _locationRepository.Add(location);

                return Results.Ok(result);
            });

            app.MapDelete("api/locations", async (ILocationRepository _locationRepository, int? id) =>
            {
                if (!id.HasValue)
                    return Results.BadRequest();

                var result = await _locationRepository.Delete(id.Value);
                return Results.Ok(result);
            });

        }
    }
}
