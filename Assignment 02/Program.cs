using Assignment_02.Controllers;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Assignment_02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();     // MVC
            //builder.Services.AddControllers();            // API
            //builder.Services.AddRazorPages();             // Razor
            //builder.Services.AddMvc();                    // For All 3 Projects


            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{Controller=Home}/{Action=Index}/{id?}"

                    //constraints: new { id = new IntRouteConstraint() },
                    //defaults: new {Controller = "Home" , Action = "Index"}
                    );
            });


            app.Run();
        }
    }
}
