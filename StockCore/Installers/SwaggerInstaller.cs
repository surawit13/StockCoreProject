namespace StockCore.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //throw new NotImplementedException();
            //services.AddControllers();
            services.AddSwaggerGen();
        }
    }
}
