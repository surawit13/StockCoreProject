using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace StockCore.Installers
{
    public class JWTInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtsetting = new JwtSettings();
            configuration.Bind(nameof(jwtsetting),jwtsetting);
            services.AddSingleton(jwtsetting); //addsingleton mean this is can bbe only one onlyyyyyy
            //throw new NotImplementedException();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtsetting.Issuer,
                        //ValidIssuer = "xxxx",
                        ValidateAudience = true,
                        ValidAudience = jwtsetting.Audience,
                        //ValidAudience = "xxxxwerwe",
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsetting.Key)),
                        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdad")),
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
        public class JwtSettings
        {
            public string Key { get; set; }
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string Expire { get; set; }
        }

          //"JwtSetting": {
          //  "Key": "QWEmxvcSDFqwezxce!23",
          //  "Issuer": "UserLocal",
          //  "Audience": "https://lnwza007.com",
          //  "Expire": "30"
          //}
    }
}
