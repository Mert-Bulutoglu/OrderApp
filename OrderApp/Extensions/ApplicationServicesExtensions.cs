using OrderApp.Caching.Services;
using OrderApp.Infrastructure.Services;
using OrderApp.Persistance.Repositories;
using OrderApp.Persistance.UnitOfWorks;
using OrderApp.RabbitMQ.Services;
using OrderApp.Repository.Cache;
using OrderApp.Repository.Repositories;
using OrderApp.Repository.Services;
using OrderApp.Repository.SMTP;
using OrderApp.Repository.UnitOfWorks;

namespace OrderApp.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion
            #region Services

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMailSenderBackgroundService, MailSenderBackgroundService>();
            services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
            services.AddScoped(typeof(IFilterService<>), typeof(FilterService<>));
            services.AddScoped(typeof(ICacheService<>), typeof(CacheService<>));
            #endregion

            return services;

        }
    }
}
