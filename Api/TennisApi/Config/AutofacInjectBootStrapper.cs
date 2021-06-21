using Autofac;
using FluentValidation;
using TennisApi.Interactors.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TennisApi.Config
{
    public class AutofacInjectBootStrapper : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyTypes(typeof(OpenExchagneRatesService).GetTypeInfo().Assembly)
            //    .Where(t => t.Name.EndsWith("Service"))
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(LoginRequest).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(AbstractValidator<>))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}
