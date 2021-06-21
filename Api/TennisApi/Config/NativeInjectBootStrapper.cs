using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisApi.Config
{
    public static class NativeInjectBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<IUnitOfWorkAsync, UnitOfWork>();

            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped(typeof(IRepositoryAsync<>), typeof(Repository<>));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            //services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain - Notifiation Events
            //services.AddScoped<AsyncNotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Infra - Data EventSourcing
            //services.AddScoped<IEventStore, SqlEventStore>();
            //services.AddScoped<IRepository, SqlRepository>();// (y => new SqlRepository(new Repository(y.GetService<IEventStore>()), y.GetService<IEventStore>(), y.GetService<ICache>()));
            //services.AddScoped<Domain.Core.Domain.ISession, Session>();

            //services.AddSingleton<IBusinessListingSearch, BusinessListingSearch>();
        }
    }
}
