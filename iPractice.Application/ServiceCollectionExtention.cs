using System;
using System.Linq;
using System.Reflection;
using FluentValidation;
using iPractice.Abstraction.Handler;
using iPractice.Abstraction.Validation;
using iPractice.Application.Commands.CreateAppointment;
using iPractice.Application.CQRS;
using iPractice.Application.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace iPractice.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.AddMediator();
            services.AddHandlers();
            services.AddValidators();
            return services;
        }

        private static void AddValidators(this IServiceCollection services)
        {
            static bool Expression(Type type) => type.Is(typeof(IValidator<>));

            var validators = Assembly.GetAssembly(typeof(CreateAppointmentCommandValidator))?.GetTypes()
                .Where(type => type.GetInterfaces().Any(Expression) && !type.IsAbstract && type.IsClass);

            if (validators == null) return;
            foreach (var v in validators)
            {
                var interfaces = v.GetInterfaces().Where(Expression);
                foreach (var i in interfaces)
                {
                    services.AddScoped(i, v);
                }
            }
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            services.AddScoped<ICommandSender, CommandSender>();
            services.AddScoped<IQueryProcessor, QueryProcessor>();
        }

        private static void AddHandlers(this IServiceCollection services)
        {
            static bool Expression(Type type) => type.Is(typeof(ICommandHandler<,>) ) || type.Is(typeof(IQueryHandler<,>));

            var handlers = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.GetInterfaces().Any(Expression));

            foreach (var h in handlers)
            {
                var handlerInterfaces = h.GetInterfaces().Where(Expression);
                foreach (var hi in handlerInterfaces)
                {
                    services.AddScoped(hi, h);
                }
            }
        }

        private static bool Is(this Type type, Type typeCompare)
        {
            return type.IsGenericType && (type.Name.Equals(typeCompare.Name) || type.GetGenericTypeDefinition() == typeCompare);
        }
    }
}