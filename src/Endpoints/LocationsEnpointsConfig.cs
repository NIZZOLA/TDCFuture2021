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
            app.MapGet("api/places", async (IPlaceRepository _placeRepository) =>
            {
                var result = await _placeRepository.GetAll();
                if (result == null)
                    return Results.NotFound();

                return Results.Ok(result);
            });

            app.MapGet("api/places/{id}", async (IPlaceRepository _placeRepository, int? id) =>
            {
                if (!id.HasValue)
                    return Results.BadRequest();

                var place = await _placeRepository.GetOne(id.Value);

                return Results.Ok(place);
            });

            app.MapGet("api/places/hierarchy/{id}", async (IPlaceRepository _placeRepository, ILocationRepository _locationRepository, int? id) =>
            {
                if (!id.HasValue)
                    return Results.BadRequest();

                var response = new PlaceHierarchyResultModel();
                response.Place = await _placeRepository.GetOne(id.Value);
                response.Hierarchy = await _locationRepository.GetFromGeo(response.Place.Latitude, response.Place.Longitude);

                return Results.Ok(response);
            });

            app.MapGet("api/places/getbygeo", async (IPlaceRepository _placeRepository, double latitude, double longitude, int meters, string type) =>
            {
                var response = await _placeRepository.GetByLocation(new PlacesByGeoRequest(latitude, longitude, meters, type));
                return Results.Ok(response);
            });

            app.MapGet("api/places/getbylocation", async (IPlaceRepository _placeRepository, int locationId, string type) =>
            {
                var response = await _placeRepository.GetByLocationId(locationId, type);
                return Results.Ok(response);
            });

            app.MapPost("api/places", async (IPlaceRepository _placeRepository, PlaceModel place) =>
            {
                if (place == null)
                    return Results.BadRequest();

                var response = await _placeRepository.Add(place);

                return Results.CreatedAtRoute("GetPlaceModel", new { id = response.PlaceId }, response);
            });

            app.MapDelete("api/places/{id}", async (IPlaceRepository _placesRepository, int? id) =>
            {
                if (!id.HasValue)
                    return Results.BadRequest();

                var result = await _placesRepository.Delete(id.Value);
                return Results.Ok(result);
            });
        }
    }
}
