using Autofac;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using Inflector;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Reflection;
using TauCode.Domain.NHibernate.Conventions;

namespace TauCode.WebApi.Host.NHibernate
{
    public static class WebApiHostNHibernateExtensions
    {
        public static ISessionFactory BuildSessionFactory(
            Configuration configuration,
            Assembly mappingsAssembly,
            Type idUserTypeGenericType)
        {
            // todo: check args

            return Fluently.Configure(configuration)
                .Mappings(m => m.FluentMappings.AddFromAssembly(mappingsAssembly)
                    .Conventions.Add(ForeignKey.Format((p, t) =>
                    {
                        if (p == null) return t.Name.Underscore() + "_id";

                        return p.Name.Underscore() + "_id";
                    }))
                    .Conventions.Add(LazyLoad.Never())
                    .Conventions.Add(Table.Is(x => x.TableName.Underscore().ToUpper()))
                    .Conventions.Add(ConventionBuilder.Property.Always(x => x.Column(x.Property.Name.Underscore())))
                    .Conventions.Add(new IdUserTypeConvention(idUserTypeGenericType))
                )
                .BuildSessionFactory();
        }

        public static IAppStartup AddNHibernate(
            this IAppStartup appStartup,
            Configuration configuration,
            Assembly mappingsAssembly,
            Type idUserTypeGenericType)
        {
            // todo: check args

            appStartup.GetContainerBuilder()
                .Register(c => BuildSessionFactory(configuration, mappingsAssembly, idUserTypeGenericType))
                .As<ISessionFactory>()
                .SingleInstance();

            appStartup.GetContainerBuilder()
                .Register(c => c.Resolve<ISessionFactory>().OpenSession())
                .As<ISession>()
                .InstancePerLifetimeScope();

            return appStartup;
        }
    }
}
