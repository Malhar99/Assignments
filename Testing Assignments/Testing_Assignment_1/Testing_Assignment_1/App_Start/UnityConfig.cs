using System.Web.Http;
using Testing_Assignment_1.Interface;
using Testing_Assignment_1.Manager;
using Unity;
using Unity.WebApi;

namespace Testing_Assignment_1
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IPassengerManager, PassengerManager>();
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}