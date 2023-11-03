using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace OlympicGames.Identity;

public static class Extensions
{
    public static AuthenticationBuilder AddJwtBearerAuthentication(this IServiceCollection services)
    {
        return services.ConfigureOptions<ConfigureJwtBearerOptions>()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
    }
}