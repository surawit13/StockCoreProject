namespace StockCore.Installers
{
    public class CORSInstallere : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    builder.WithOrigins(
                        "https://www.w3schools.com", 
                        "https://localhost.com:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
                options.AddPolicy("Getonly", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET");
                });

                options.AddPolicy("PostOnly", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().WithMethods("POST");
                });
            });
        }
    }
}
