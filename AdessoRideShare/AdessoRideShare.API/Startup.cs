using AdessoRideShare.API.CustomException;
using AdessoRideShare.API.Filter;
using AdessoRideShare.DataAccess.UnitOfWork;
using AdessoRideShare.Domain;
using AdessoRideShare.Domain.Context;
using AdessoRideShare.Domain.IContext;
using AdessoRideShare.Service.IServices;
using AdessoRideShare.Service.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace AdessoRideShare.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, AdessoUnitOfWork>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IJourneyService, JourneyService>();
            services.AddTransient<IJourneyBookingService, JourneyBookingService>();
            services.AddTransient<IJourneyRouteService, JourneyRouteService>();
            services.AddTransient<ICityService, CityService>();

            services.AddDbContext<AdessoDbContext>(opts =>
                                                        opts.UseInMemoryDatabase("AdessoDB"));

            services.AddScoped<IAdessoDbContext>(provider => provider.GetService<AdessoDbContext>());

            services.AddControllers();

            // ModelState kontrolü
            services.AddMvcCore(options =>
            {
                options.Filters.Add(typeof(ValidateModelFilter));
            });


            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Startup));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IAdessoDbContext context)
        {
            // In memory database kullandığım için  test verisi ekliyorum.
            AddTestData(context);

            // Dosyaya log yazmak için
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Adesso Ride Share API V1");
            });

            // Exception handlerin eklenmesi.
            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddTestData(IAdessoDbContext context)
        {
            var testUser1 = new User
            {
                Id = new Guid("efe997f6-823e-4ad2-a7df-22ff0ab59eac"),
                Name = "Cihan",
                Surname = "Akkurt"
            };
            var testUser2 = new User
            {
                Id = new Guid("60e709f3-4f3b-4833-838d-296a9e345b6d"),
                Name = "Bulut",
                Surname = "Akkurt"
            };
            var testUser3 = new User
            {
                Id = new Guid("bc1bf525-5234-467f-b743-7241487bc3d2"),
                Name = "Sinem",
                Surname = "Akkurt"
            };

            context.Users.Add(testUser1);
            context.Users.Add(testUser2);
            context.Users.Add(testUser3);

            var testCity1 = new City
            {
                Id = new Guid("91ac884f-815b-4410-a479-a43f71316e20"),
                Name = "Adana",
                Code = 1
            };
            var testCity2 = new City
            {
                Id = new Guid("48ceeaec-6e21-449d-b1c9-90224f4964c1"),
                Name = "Adıyaman",
                Code = 2
            };
            var testCity3 = new City
            {
                Id = new Guid("3d81916c-bacb-44da-bac9-5dc0def88091"),
                Name = "Afyonkarahisar",
                Code = 3
            };
            var testCity4= new City
            {
                Id = new Guid("02d23e22-8ce9-4ab6-a221-06373e1f7653"),
                Name = "Ağrı",
                Code = 4
            };
            var testCity5 = new City
            {
                Id = new Guid("185b6169-f2c7-4bd2-aa5a-85ffd3c22fe9"),
                Name = "Amasya",
                Code = 5
            };
            var testCity6 = new City
            {
                Id = new Guid("a3de0d33-fb56-461a-9d4b-3c9143e75b89"),
                Name = "Ankara",
                Code = 6
            };
            var testCity7 = new City
            {
                Id = new Guid("99203193-3297-4030-b714-e3947f3c609d"),
                Name = "Antalya",
                Code = 7
            };

            context.Cities.Add(testCity1);
            context.Cities.Add(testCity2);
            context.Cities.Add(testCity3);
            context.Cities.Add(testCity4);
            context.Cities.Add(testCity5);
            context.Cities.Add(testCity6);
            context.Cities.Add(testCity7);

            context.SaveChangesAsync();
        }
    }
}
