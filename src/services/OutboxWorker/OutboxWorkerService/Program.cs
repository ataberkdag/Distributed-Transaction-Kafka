using Core.Infrastructure.Dependencies;
using OutboxWorkerService;

IConfiguration Configuration = null;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, cfg) =>
    {

        var env = hostingContext.HostingEnvironment;
        Configuration = new ConfigurationBuilder()
            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .Build();
    })
    .ConfigureServices(services =>
    {
        services.AddPostgreConnectionFactory(Configuration.GetConnectionString("OrderDb"), "OrderDb");
        services.AddPostgreConnectionFactory(Configuration.GetConnectionString("StockDb"), "StockDb");

        services.AddBrokerDependency(options => {
            options.MessageServiceType = MessageServiceType.Producer;
            options.BrokerAddress = Configuration.GetValue<string>("Kafka:BootstrapServers");
        });

        services.AddHostedService<OrderOutboxWorker>();
        services.AddHostedService<StockOutboxWorker>();
    })
    .Build();

await host.RunAsync();
