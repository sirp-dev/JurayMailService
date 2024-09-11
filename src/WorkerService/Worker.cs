using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        //private readonly Domain.Interfaces.IEmailSendingStatusRepository _emailSending;
        public Worker(ILogger<Worker> logger/*, Domain.Interfaces.IEmailSendingStatusRepository emailSending*/, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            //_emailSending = emailSending;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var emailSendingStatusRepository = scope.ServiceProvider.GetRequiredService<IEmailSendingStatusRepository>();

                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        await emailSendingStatusRepository.SendBatchEmailByEmailIds();
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    }
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
