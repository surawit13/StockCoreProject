using Autofac;
using System.Reflection;

namespace StockCore.AutoFac
{
    public class AutofaceModule 
    {
        public  AutofaceModule(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }
    }
}
