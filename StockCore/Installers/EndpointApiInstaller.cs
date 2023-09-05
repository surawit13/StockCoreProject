namespace StockCore.Installers
{
    public class EndpointApiInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //throw new NotImplementedException();
            services.AddEndpointsApiExplorer();
        }
    }
}
