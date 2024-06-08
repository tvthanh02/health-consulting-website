

using health_consulting_website.Models;
using Microsoft.AspNetCore.Http.HttpResults;

public class AuthorizationMiddleware
{

    private readonly RequestDelegate _next;

    public AuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    public async Task Invoke(HttpContext context)
    {

        string userCurrentRole = context.Items["u_role"] as string;

        //Console.WriteLine(string.IsNullOrEmpty(userCurrentRole));

        if (!string.IsNullOrEmpty(userCurrentRole))
        {
            switch (userCurrentRole)
            {
                case "ADMIN": IsAdmin(context); break;
                case "DOCTOR": IsDoctor(context); break;
                case "USER": IsUser(context); break;
            }
        }
        await _next(context);
    }

    private void IsAdmin(HttpContext context)
    {

        var allowAccessRouteAdmin = new List<string>(){
            "/he-thong/admin",
        };

        if (!IsValidAccessRoute(context.Request.Path, allowAccessRouteAdmin))
        {
            context.Response.Redirect("/he-thong/admin");
        }
    }
    private void IsDoctor(HttpContext context)
    {
        var allowAccessRouteDoctor = new List<string>(){
            "/he-thong/cms",
        };

        if (!IsValidAccessRoute(context.Request.Path, allowAccessRouteDoctor))
        {
            context.Response.Redirect("/he-thong/cms");
        }
    }
    private void IsUser(HttpContext context)
    {
        var denyAccessRouteUser = new List<string>(){
            "/he-thong/cms",
            "/he-thong/admin",
            "/dang-nhap"
        };

        if (IsValidAccessRoute(context.Request.Path, denyAccessRouteUser))
        {
            context.Response.Redirect("/");
        }
    }

    private bool IsValidAccessRoute(PathString path, List<string> allowAccessRoute)
    {

        var absolutePath = path.Value.ToLower();
        return allowAccessRoute.Any(route => absolutePath.StartsWith(route.ToLower()));
    }

}