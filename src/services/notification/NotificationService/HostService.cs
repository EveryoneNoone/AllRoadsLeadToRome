using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService
{
    internal class HostService : IHostedService
    {
        private IBusControl busControl;
        private readonly ILogger<HostService> logger;

        public HostService(IBusControl busControl, ILogger<HostService> logger)
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
