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
using LocationMinApi.Endpoints;

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

LocationEnpointsConfig.AddEndpoints(app);
PlacesEnpointsConfig.AddEndpoints(app);

app.Run();

public record GeoRequest(double Latitude, double Longitude);

public record LocationRequest(string LocationName, string LocationCode, double Latitude, double Longitude, string Polygon, int LocationType);

public record PlacesByGeoRequest( double Latitude, double Longitude, int Meters, string Type );
