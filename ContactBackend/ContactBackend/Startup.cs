using Contact.Core.Interfaces.Contact;
using Contact.Infrastructure.Repository.Contact;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Contact.API.Helper;
using Contact.Infrastructure.Data;
using Contact.Core.Interfaces.Phone;
using Contact.Infrastructure.Repository.Phone;
using Contact.Core.Interfaces.Auth;
using Contact.Infrastructure.Services;
using Contact.API.Extensions;

namespace ContactBackend
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
            services.AddAuthentication(Configuration);

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy",
                policy =>
                {
                    policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("https://localhost:4200");
                });
            });

            services.AddControllers();

            services.AddAutoMapper(typeof(AutoMapping));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContactBackend", Version = "v1" });
            });
            
            //services.AddDbContext<AppDbContext>(
            //    options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppDbContext>();

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IAccountService, AccountService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactBackend v1"));
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
