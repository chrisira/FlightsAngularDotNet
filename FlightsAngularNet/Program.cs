using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.OpenApi.Models;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

// configuring the swagger server
builder.Services.AddSwaggerGen( c =>

{
    c.AddServer(new OpenApiServer
    {

        Description = "Development Server",
        Url = "https://localhost:7129/"
    });
    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"] + e.ActionDescriptor.RouteValues["controller"]}");
});


var app = builder.Build();

// adding the CORS

app.UseCors(builder => builder.WithOrigins("*"));

// the withOrigines("*") means that all the external applications will be able to access the .net application 

// configuring the swagger UI
app.UseSwagger().UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
