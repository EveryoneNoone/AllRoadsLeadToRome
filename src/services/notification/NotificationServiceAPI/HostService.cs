
using MassTransit;

namespace NotificationServiceAPI
{
    public class HostService : IHostedService
    {
        private IBusControl busControl;
        private readonly ILogger<HostService> logger;

        public HostService(ILogger<HostService> logger, IBusControl busControl)
        {
            this.busControl = busControl;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await busControl.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await busControl.StopAsync(cancellationToken);
        }
    }
}
