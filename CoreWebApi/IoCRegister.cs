using System;
using CoreWebApi.Controllers;
using Data.Repository.Interface;
using Data.Repository.Service;
using LightInject;
using Business.Interface;
using Business.Service;

namespace CoreWebApi
{
    public static class IoCRegister
    {
        //Configures ServiceContainer instance 
        public static void Configure(ServiceContainer container)
        {
            container.RegisterControllers();
            container.RegisterApiControllers();
            container.EnableAnnotatedConstructorInjection();//Dot net core does not allow Property Injection that's why it's been used enabled.
            Register(container);
            container.EnableMvc();
        }

        public static void Register(ServiceContainer container)
        {
            container.Register<IProductRepsitory, ProductRepository>(new PerContainerLifetime());
            container.RegisterConstructorDependency<IProductRepsitory>((factory, propertyInfo) => container.GetInstance<IProductRepsitory>());
            container.Register<IProductService, ProductService>(new PerContainerLifetime());
            container.RegisterConstructorDependency<IProductService>((factory, propertyInfo) => container.GetInstance<IProductService>());

        }


    }
}
