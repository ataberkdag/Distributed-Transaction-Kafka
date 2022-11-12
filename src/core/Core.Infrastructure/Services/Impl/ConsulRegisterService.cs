using Consul;
using Core.Infrastructure.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.Services.Impl
{
    public class ConsulRegisterService : IHostedService
    {
        private readonly IConsulClient _consulClient;
        private readonly ConsulConfig _consulConfig;

        public ConsulRegisterService(IConsulClient consulClient, IOptions<ConsulConfig> consulConfig)
        {
            this._consulClient = consulClient;
            this._consulConfig = consulConfig.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await this._consulClient.Agent.ServiceDeregister(this._consulConfig.ServiceId, cancellationToken);

            var serviceAddressUri = new Uri(this._consulConfig.ServiceAddress);
            var serviceRegistration = new AgentServiceRegistration
            {
                ID = this._consulConfig.ServiceId,
                Name = this._consulConfig.ServiceName,
                Address = serviceAddressUri.Host,
                Port = serviceAddressUri.Port,
            };

            await this._consulClient.Agent.ServiceRegister(serviceRegistration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await this._consulClient.Agent.ServiceDeregister(this._consulConfig.ServiceId, cancellationToken);
        }
    }
}
