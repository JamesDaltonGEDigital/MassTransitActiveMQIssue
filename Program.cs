using System.Threading.Tasks;
using MassTransit;
using MassTransit.ActiveMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace MassTransitActiveMQIssue
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.json", false);
                })
                .ConfigureLogging((context, builder) =>
                {
                    builder.ClearProviders();
                    builder.SetMinimumLevel(LogLevel.Trace);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddMassTransit(cfg =>
                    {
                        cfg.AddConsumer<ValueConsumer>();
                        cfg.AddBus(provider => Bus.Factory.CreateUsingActiveMq(cfg =>
                        {
                            cfg.AutoDelete = true;
                            var host = cfg.Host(context.Configuration["host"], hst =>
                            {
                                hst.Username(context.Configuration["userName"]);
                                hst.Password(context.Configuration["password"]);
                            });
                            cfg.ReceiveEndpoint(e =>
                            {
                                e.Consumer<ValueConsumer>(provider);
                            });
                        }));
                    });
                    services.AddHostedService<BussWorker>();
                })
                .UseNLog()
                .UseConsoleLifetime()
                .Build()
                .RunAsync();
        }
    }
}
