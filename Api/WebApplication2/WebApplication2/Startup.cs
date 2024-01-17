using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MongoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfiguratingServices(IServiceCollection services)
        { 
            var MyAllowSpecificOrigins = "AllowOrigin";

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, policy => policy.AllowAnyOrigin()
                                                                                .AllowAnyMethod()
                                                                                .AllowAnyHeader());
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(optoins => optoins.SerializerSettings.ContractResolver
                = new DefaultContractResolver());

            services.AddControllers();

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            // Configure the HTTP request pipeline.
            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }

}
