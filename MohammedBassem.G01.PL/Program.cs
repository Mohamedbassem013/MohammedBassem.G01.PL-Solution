using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MohammedBassem.G01.BLL;
using MohammedBassem.G01.BLL.interfaces;
using MohammedBassem.G01.BLL.Repositories;
using MohammedBassem.G01.DAL.Data.Contexts;
using MohammedBassem.G01.DAL.Models;
using MohammedBassem.G01.PL.Mapping.Employees;
using System.Reflection;

namespace MohammedBassem.G01.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //-------------------------------------------------------------

            //builder.Services.AddScoped<AppDbContext>(); // Allow Dependancy Ingecation For AppDbContext

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); // Allow Dependancy Ingecation For AppDbContext --> AppDbContext ل Create يعمل CLR هنا عملت حقن عشان اخلي ال

            //-------------------------------------------------------------

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();//Allow Dependancy Ingecation For DepartmentRepository

            ////-------------------------------------------------------------
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();//Allow Dependancy Ingecation For EmployeeRepository
            //-------------------------------------------------------------
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            //-------------------------------------------------------------
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddDefaultTokenProviders();//Allow Dependancy Ingecation For Identity Role
            //-------------------------------------------------------------
            // التلاته نفس الحاجه
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //-------------------------------------------------------------

            // Life Time
            //builder.Services.AddScoped();    // LifeTime Rar Request , Object UnReachable
            //builder.Services.AddTransient(); // LifeTime Rar Operations
            //builder.Services.AddSingleton(); // LifeTime Rar Application

            //builder.Services.AddScoped<IScopedServices , ScopedServices>();         // Par Request
            //builder.Services.AddTransient<ITranientServices, TranientServices>();   // Par Operations
            //builder.Services.AddSingleton<ISingletonServices, SingletonServices>(); // Par App


            //-------------------------------------------------------------
            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/sIgnIn";
                });
            //-------------------------------------------------------------

            var app = builder.Build();
            //-------------------------------------------------------------

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //-------------------------------------------------------------

            app.UseStaticFiles();

            //-------------------------------------------------------------

            app.UseRouting();

            //-------------------------------------------------------------
            //UseRouting لازم يكونو بعد ال 
          
            app.UseAuthentication();
			app.UseAuthorization();

			//-------------------------------------------------------------

			app.MapControllerRoute
            (
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}
