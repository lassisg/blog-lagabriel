using Data.Models;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebApp.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryApi(this WebApplication app)
    {
        app.MapGet("/api/Categories",
                   async (IBlogApi api)
                       => Results.Ok((object?)await api.GetCategoriesAsync()));
        
        app.MapGet("/api/Categories/{*id}",
                   async (IBlogApi api, string id) 
                       => Results.Ok((object?)await api.GetCategoryAsync(id)));
        
        app.MapPut("/api/Categories",
                   async (IBlogApi api, [FromBody] Category item) 
                       => Results.Ok((object?)await api.SaveCategoryAsync(item)))
            // .RequireAuthorization()
            ;
        
        app.MapDelete("/api/Categories/{*id}",
                      async (IBlogApi api, string id) =>
                      {
                          await api.DeleteCategoryAsync(id);
                          return Results.Ok();
                      })
            // .RequireAuthorization()
            ;
    }
}