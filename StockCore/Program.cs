using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StockCore.Data;
using Mapster;
using StockCore.Interfaces;
using StockCore.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using StockCore.AutoFac;
using System.Reflection;
using StockCore.Installers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddMapster();

//option 1 using autofac and install service auto if  lastname is contains "Service"
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
    .Where(t => t.Name.EndsWith("Service"))
    .AsImplementedInterfaces();
});

//builder.Services.AddDbContext<DatabaseContext>(options => 
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Local_Mssql")));
builder.Services.InstallServiceInAssembly(builder.Configuration);

//option 2 manully resigter service
//builder.Services.AddTransient<IProductService, ProductService>(); // basic register services



//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); /// add url can access folder  images in wwwroot/  "https://localhost:44385/images/06e03abc-4abf-4c66-adea-13b4a99cd737.jpg"
app.UseCors("AllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
