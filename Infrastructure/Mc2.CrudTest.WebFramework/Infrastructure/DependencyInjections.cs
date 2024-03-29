﻿using Mc2.CrudTest.Application.Cqrs.Customers.Services;
using Mc2.CrudTest.Application.Interfaces;
using Mc2.CrudTest.Persistence.Context;
using Mc2.CrudTest.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Mc2.CrudTest.Application.Cqrs.Customers.Behaviors;
using Mc2.CrudTest.Application.Cqrs.Customers.Queries;

namespace Mc2.CrudTest.WebFramework.Infrastructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(GetAllCustomersQuery).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddScoped<IMc2DbContext>(provider => provider.GetService<Mc2Context>()!);
            services.AddDbContext<Mc2Context>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IMc2Repository<>), typeof(Mc2Repository<>));
            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}
