using BlazorToDoApp.Data;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorToDoApp.Data.Entities;
using BlazorToDoApp.Data.Repositories;
using BlazorToDoApp.Data.Repositories.Contracts;
using BlazorToDoApp.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// make a connection string
string conn = DevExpress.Xpo.DB.MySqlConnectionProvider.GetConnectionString("localhost", "todouser", "FrId.225", "todoapp");
XpoDefault.DataLayer = XpoDefault.GetDataLayer(conn, AutoCreateOption.DatabaseAndSchema);

// Inject services
builder.Services.AddSingleton(XpoDefault.DataLayer);
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<Repository<ToDoItem>>();
builder.Services.AddScoped<Repository<Category>>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ToDoService>();

builder.Services.AddDevExpressBlazor(options => {
    options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
    options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});
builder.WebHost.UseWebRoot("wwwroot");
builder.WebHost.UseStaticWebAssets();

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
app.MapFallbackToPage("/_Host");

app.Run();