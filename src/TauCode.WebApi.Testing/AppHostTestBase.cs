using Autofac;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using NHibernate;
using System;
using System.Linq;
using System.Net.Http;
using TauCode.Db.Testing;
using TauCode.WebApi.Host;
using TauCode.WebApi.Testing.Exceptions;

namespace TauCode.WebApi.Testing
{
    public abstract class AppHostTestBase<TStartup, TFactory> : DbTestBase
        where TStartup : class
        where TFactory : WebApplicationFactory<TStartup>
    {
        protected TFactory Factory { get; set; }
        protected HttpClient HttpClient { get; set; }

        protected IContainer Container { get; set; }
        protected ILifetimeScope SetupLifetimeScope { get; set; }
        protected ILifetimeScope TestLifetimeScope { get; set; }
        protected ILifetimeScope AssertLifetimeScope { get; set; }

        protected ISession SetupSession { get; set; }
        protected ISession TestSession { get; set; }
        protected ISession AssertSession { get; set; }

        protected abstract string GetSolutionRelativeContentRoot();

        protected virtual TFactory CreateFactory()
        {
            var type = typeof(TFactory);
            var ctor = type.GetConstructor(Type.EmptyTypes);
            if (ctor == null)
            {
                throw new WebApiTestingException(
                    $"Type '{typeof(TFactory).FullName}' doesn't have a default constructor.");
            }

            var factory = (TFactory)ctor.Invoke(new object[] { });
            return factory;
        }

        protected virtual HttpClient CreateHttpClient()
        {
            return this.Factory
                .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(this.GetSolutionRelativeContentRoot()))
                .CreateClient();
        }

        protected virtual TestServer GetTestServer()
        {
            return this.Factory.Factories.Single().Server;
        }

        protected virtual IContainer GetContainer()
        {
            var testServer = this.GetTestServer();
            var appStartup = (IAppStartup)testServer.Host.Services.GetService(typeof(IAppStartup));
            var container = appStartup.GetContainer();
            return container;
        }

        protected override void OneTimeSetUpImpl()
        {
            this.Factory = this.CreateFactory();
            this.HttpClient = this.CreateHttpClient();
            this.Container = this.GetContainer();

            // Yes, exactly. first, we setup IoC/Web API things, and then - DB stuff from the super-class.
            // Because GetConnectionString might want to use TestStartup's connection string, and TestStartup is injected via IoC.
            base.OneTimeSetUpImpl();
        }

        protected override void OneTimeTearDownImpl()
        {
            base.OneTimeTearDownImpl();

            this.HttpClient.Dispose();
            this.Factory.Dispose();

            this.HttpClient = null;
            this.Factory = null;
        }

        protected override void SetUpImpl()
        {
            // autofac stuff
            this.SetupLifetimeScope = this.Container.BeginLifetimeScope();
            this.TestLifetimeScope = this.Container.BeginLifetimeScope();
            this.AssertLifetimeScope = this.Container.BeginLifetimeScope();

            // nhibernate stuff
            this.SetupSession = this.SetupLifetimeScope.Resolve<ISession>();
            this.TestSession = this.TestLifetimeScope.Resolve<ISession>();
            this.AssertSession = this.AssertLifetimeScope.Resolve<ISession>();
        }

        protected override void TearDownImpl()
        {
            this.SetupSession.Dispose();
            this.TestSession.Dispose();
            this.AssertSession.Dispose();

            this.SetupLifetimeScope.Dispose();
            this.TestLifetimeScope.Dispose();
            this.AssertLifetimeScope.Dispose();
        }
    }
}
