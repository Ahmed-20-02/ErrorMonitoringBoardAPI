namespace DevelopmentProjectErrorBoardAPI.Business.AutofacDependencies
{
    using System.Reflection;
    using Autofac;

    public class AutofacBusinessModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();
        }
    }
}

//Reference
//https://stackoverflow.com/questions/69754985/adding-autofac-to-net-core-6-0-using-the-new-single-file-template/71448702#71448702
//13/03/2024