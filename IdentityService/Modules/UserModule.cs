using IdentityService.DTO;
using IdentityService.Entities;

using Microsoft.AspNetCore.Identity;

namespace IdentityService.Modules;

public static class UserModule
{
    public static void RegisterUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/user", (UserManager<ApplicationUser> _userManager) =>
        {
            var list = _userManager.Users.ToList();
            return Results.Ok(list);
        });

        app.MapPost("/user", async (NewUserInput user, UserManager<ApplicationUser> _userManager) =>
        {
            ApplicationUser applicationUser = new()
            {
                Email = user.Email,
                UserName = user.Email
            };

            var result = await _userManager.CreateAsync(applicationUser, user.Password);

            return Results.NoContent();
        });
    }
}
