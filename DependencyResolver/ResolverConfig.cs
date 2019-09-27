using BLL.Interface.Interfaces;
using BLL.ServiceImplementation;
using DAL.Interface.Interfaces;
using DAL.Repositories;
using Ninject;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IRepository>().To<AccountRepository>().WithConstructorArgument(@"R:\states.bin");
            kernel.Bind<IAccountGenerateIdNumber>().To<AccountGenerateId>();
        }
    }
}