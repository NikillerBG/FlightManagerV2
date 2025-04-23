using DataLayer;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FlightManager.BlazorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔌 Configure services
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // 📦 Register ApplicationDbContext with SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString(@"Server=LEGION\\SQLEXPRESS;Database=FlightDB;Trusted_Connection=True;TrustServerCertificate=True")));

            // 🧠 Register service/repository layer
            builder.Services.AddScoped<FlightContext>();
            builder.Services.AddScoped<PassengerContext>();
            builder.Services.AddScoped<PlaneContext>();
            builder.Services.AddScoped<ReservationContext>();


            builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            builder.Services.AddScoped<UserManager<User>>();


            // 🚀 Build the app
            var app = builder.Build();

            // 🛠 Configure middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // 🧭 Routing for Blazor
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            // 🏁 Run
            app.Run();
        }
    }
}