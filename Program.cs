using MVCAssignmentThree.Repository;
using MVCAssignmentThree.Service;

namespace MVCAssignmentThree
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //register all custom service like connectionString,repository,services

            //1 - add connection string
            var connectionString = builder.Configuration.GetConnectionString("MVCConnectionString");

            // Register DI for your repositories and services
            builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
            builder.Services.AddScoped<IUserService, UserServiceImpl>();

            // Add configuration access (to read connection strings)
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Login}/{id?}"
            );

            app.Run();
        }
    }
}
