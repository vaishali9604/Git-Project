using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.Configuration;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.DataLayer.Repository;
using AutoMapper;

namespace MT.OnlineRestaurant.ReviewManagement
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IHostingEnvironment env)
        {
            _applicationPath = env.WebRootPath;
            _contentRootPath = env.ContentRootPath;
            // Setup configuration sources.

            var builder = new ConfigurationBuilder()
                .SetBasePath(_contentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                // builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        private static string _applicationPath = string.Empty;
        private static string _contentRootPath = string.Empty;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info { Title = "SearchManager", Version = "1.0" });
                //c.OperationFilter<HeaderFilter>();
            });

            //services.Configure<ConnectionString>(Configuration.GetSection("ConnectionString"));

            services.AddDbContext<RestaurantManagementContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectionString")));
              // b => b.MigrationsAssembly("MT.OnlineRestaurant.DataLayer")));

            services.AddMvc()
                    .AddMvcOptions(options =>
                    {
                        //options.Filters.Add(new Authorization());
                        //options.Filters.Add(new LoggingFilter(Configuration["ConnectionString:DatabaseConnectionString"]));
                        //options.Filters.Add(new ErrorHandlingFilter(Configuration["ConnectionString:DatabaseConnectionString"]));
                        //options.Filters.Add(new LoggingFilter(Configuration.GetConnectionString("DatabaseConnectionString")));
                        //options.Filters.Add(new ErrorHandlingFilter(Configuration.GetConnectionString("DatabaseConnectionString")));

                    });

            services.AddTransient<IRestaurantBuisness,RestaurantBuisness>();
            services.AddTransient<IReviewRespository,ReviewRepository>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "SearchManager (V 1.0)");
            });
            app.UseMvc();
            //message.RegisterOnMessageHandlerAndReceiveMessages();
        }
    }
}
