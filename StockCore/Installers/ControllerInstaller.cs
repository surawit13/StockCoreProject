namespace StockCore.Installers
{
    public class ControllerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //throw new NotImplementedException();
            services.AddControllers();
        }
    }
}
