using Data.Models;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Endpoints;

public static class TagEndpoints
{
    public static void MapTagApi(this WebApplication app)
    {
        app.MapGet("/api/Tags",
                   async (IBlogApi api) 
                       => Results.Ok((object?)await api.GetTagsAsync()));
        
        app.MapGet("/api/Tags/{*id}",
                   async (IBlogApi api, string id) 
                       => Results.Ok((object?)await api.GetTagAsync(id)));
        
        app.MapPut("/api/Tags",
                   async (IBlogApi api, [FromBody] Tag item) 
                       => Results.Ok((object?)await api.SaveTagAsync(item)))
            // .RequireAuthorization()
            ;
        
        app.MapDelete("/api/Tags/{*id}",
                      async (IBlogApi api, string id) =>
                      {
                          await api.DeleteTagAsync(id);
                          return Results.Ok();
                      })
            // .RequireAuthorization()
            ;
    }
}