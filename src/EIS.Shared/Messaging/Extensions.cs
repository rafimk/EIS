using EIS.Shared.Messaging.Brokers;
using EIS.Shared.Messaging.Clients;
using EIS.Shared.Messaging.Exceptions;
using EIS.Shared.Messaging.Streams;
using EIS.Shared.Messaging.Streams.Serialization;
using EIS.Shared.Messaging.Subscribers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EIS.Shared.Messaging;

public static class Extensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("messaging");
        services.Configure<MessagingOptions>(section);
        services.ConfigureOptions<MessagingOptions>();
        services.AddTransient<IMessageBroker, MessageBroker>();
        services.AddSingleton<IMessageBrokerClient, DefaultMessageBrokerClient>();
        services.AddSingleton<IMessageSubscriber, DefaultMessageSubscriber>();
        services.AddSingleton<IMessagingExceptionPolicyResolver, DefaultMessagingExceptionPolicyResolver>();
        services.AddSingleton<IMessagingExceptionPolicyHandler, DefaultMessagingExceptionPolicyHandler>();
        services.AddSingleton<IStreamSerializer, SystemTextJsonStreamSerializer>();
        services.AddSingleton<IStreamPublisher, DefaultStreamPublisher>();
        services.AddSingleton<IStreamSubscriber, DefaultStreamSubscriber>();
        
        return services;
    }
    
    public static IMessageSubscriber Subscribe(this IApplicationBuilder app)
        => app.ApplicationServices.GetRequiredService<IMessageSubscriber>();
}