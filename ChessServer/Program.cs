using ChessServer.Data;
using DataAccess.DbAccess;
using DataAccessLibrary.Data;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.ResponseCompression;
using ChessServer.Hubs;
using ChessServer.Hubs.GamePlay;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IPlayerData, PlayerData>();
builder.Services.AddSingleton<IGameData, GameData>();

//draganddrop
builder.Services.AddSingleton<DragAndDropService>();

//hubs
builder.Services.AddSingleton<GameManager>();
builder.Services.AddSingleton<PlayManager>();


builder.Services.AddScoped<PlayerModel>();
builder.Services.AddTransient<ChessBoard>();

//builder.Services.AddResponseCompression(opts =>
    
//);

builder.Services.AddSignalR();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();



app.MapBlazorHub();
app.MapHub<GameHub>("/gamehub/Id={Id}");
app.MapHub<PlayHub>("/playhub");
app.MapFallbackToPage("/_Host");

app.Run();
