using AutoMapper;
using Mallconomy.LeaderboardApp.Data.Configurations;
using Mallconomy.LeaderboardApp.Data.Entities;
using Mallconomy.LeaderboardApp.Data.Interfaces;
using Mallconomy.LeaderboardApp.Data.Services;
using Mallconomy.LeaderboardApp.Mappings.AutoMapper;
using Mallconomy.LeaderboardApp.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.Configure<MallconomyDatabaseSettings>(builder.Configuration.GetSection("MallconomyDatabase"));
builder.Services.AddScoped<IService<Leaderboard>, Service<Leaderboard>>();
builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();

var configuration = new MapperConfiguration(opt =>
{
    opt.AddProfile(new LeaderboardProfile());
});

var mapper = configuration.CreateMapper();

builder.Services.AddSingleton(mapper);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();