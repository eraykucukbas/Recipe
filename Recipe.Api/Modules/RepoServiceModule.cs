using Autofac;
using System.Reflection;
using Recipe.API.Services;
using Recipe.Core.Interfaces.Repositories;
using Recipe.Core.Interfaces.Services;
using Recipe.Core.Interfaces.UnitOfWorks;
// using Recipe.Infrastructure.Mappings;
using Recipe.Infrastructure.Models;
using Recipe.Infrastructure.Repositories;
using Recipe.Infrastructure.UnitOfWorks;
using Module = Autofac.Module;
namespace Recipe.API.Modules
{
    public class RepoServiceModule:Module
    {

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();


            // Assembly registrations
            
            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext))!;
            // var serviceAssembly = Assembly.GetAssembly(typeof(MyMapper))!;

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Custom mapper registration
            
            // var mapperAssembly = Assembly.GetAssembly(typeof(UserMapper))!;
            // builder.RegisterAssemblyTypes(mapperAssembly)
            //     .Where(t => t.Name.EndsWith("Mapper"))
            //     .AsImplementedInterfaces()
            //     .InstancePerLifetimeScope();
            
            // If you have other specific services with custom implementations, you can register them here
            // builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();
        }
    }
}
