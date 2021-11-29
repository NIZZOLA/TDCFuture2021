using LocationMinApi.Contracts;
using LocationMinApi.Data;
using LocationMinApi.Interfaces;
using LocationMinApi.Models;
using LocationMinApi.Repositories;
using LocationMinApi.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("SpatialDataEntityContext")));

builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

#region Locations
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
#endregion

#region Places
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
    var response = await _placeRepository.GetByLocation(new PlacesByGeoRequest(latitude,longitude, meters, type));
    return Results.Ok(response);
});

/*  - não aceita como get passar um objeto via Body 
 *  
app.MapGet("api/places/getbygeo", async (IPlaceRepository _placeRepository, PlacesByGeoRequest placesRequest) =>
{
    var response = await _placeRepository.GetByLocation(placesRequest);
    return Results.Ok(response);
});
*/
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
#endregion

app.Run();

public record GeoRequest(double Latitude, double Longitude);

public record LocationRequest(string LocationName, string LocationCode, double Latitude, double Longitude, string Polygon, int LocationType);

public record PlacesByGeoRequest( double Latitude, double Longitude, int Meters, string Type );
