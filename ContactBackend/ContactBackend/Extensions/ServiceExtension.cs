using Contact.Core.Interfaces.Auth;
using Contact.Core.Interfaces.Contact;
using Contact.Core.Interfaces.Phone;
using Contact.Infrastructure.Repository.Contact;
using Contact.Infrastructure.Repository.Phone;
using Contact.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Contact.API.Extensions
{
    public static class ServiceExtension
    {
        public static void RegisterApplicatonServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(Configuration);
            services.ConfigureDatabase(Configuration);
        }

        public static void AddAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });
        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            //services.AddDbContext<AppDbContext>(
            //    options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppDbContext>();

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
