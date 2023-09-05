using Microsoft.EntityFrameworkCore;
using StockCore.Data;

namespace StockCore.Installers
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //throw new NotImplementedException();
            //services.AddControllers();
            //services.AddSwaggerGen();
            services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Local_Mssql")));
        }
    }
}

