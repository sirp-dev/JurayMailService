using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JurayMailService.Web.Background
{
    public class BackgroundServiceJob : IHostedService, IDisposable
    {
        private readonly ILogger<BackgroundServiceJob> _logger;
        private Timer _timer1;
        private readonly IServiceScopeFactory _scopeFactory;

        public BackgroundServiceJob(ILogger<BackgroundServiceJob> logger, Timer timer1, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _timer1 = timer1;
            _scopeFactory = scopeFactory;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("MyBackgroundService is starting.");

            // Set up Timer1 to run after 1 minute
            _timer1 = new Timer(DoWork1, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(20));
            return Task.CompletedTask;
        }

        private async void DoWork1(object state)
        {
            _logger.LogInformation("Function 1 is running at: " + DateTime.Now);
            // Implement your first function's logic here
            using (var scope = _scopeFactory.CreateScope())
            {
                var emailSendingStatusRepository = scope.ServiceProvider.GetRequiredService<IEmailSendingStatusRepository>();

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    await emailSendingStatusRepository.SendBatchEmailByEmailIds();
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
            }



         }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("MyBackgroundService is stopping.");

            // Dispose of the timers
            _timer1?.Change(Timeout.Infinite, 0);
            

            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer1?.Dispose(); 
        }
    }
}
