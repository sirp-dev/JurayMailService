using Domain.Interfaces;

namespace JurayMailService.Web.Background
{

    public sealed class TimerService : IHostedService, IAsyncDisposable
    {
        //private readonly Task _completedTask = Task.CompletedTask;
        //private int _executionCount = 0;
        //private Timer? _timer;
        //private readonly IServiceScopeFactory _scopeFactory;
        private readonly Task _completedTask = Task.CompletedTask;
        private readonly ILogger<TimerService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private int _executionCount = 0;
        private Timer? _timer;

        public TimerService(ILogger<TimerService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }


        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("{Service} is running.", nameof(Background));
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));

            return _completedTask;
        }

        private async void DoWork(object? state)
        {
            int count = Interlocked.Increment(ref _executionCount);


            using (var scope = _scopeFactory.CreateScope())
            {
                var emailSendingStatusRepository = scope.ServiceProvider.GetRequiredService<IEmailSendingStatusRepository>();

                await emailSendingStatusRepository.SendBatchEmailByEmailIds();

            }


            _logger.LogInformation(
                "{Service} is working, execution count: {Count:#,0}",
                nameof(Background),
                count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "{Service} is stopping.", nameof(Background));

            _timer?.Change(Timeout.Infinite, 0);

            return _completedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (_timer is IAsyncDisposable timer)
            {
                await timer.DisposeAsync();
            }

            _timer = null;
        }
    }
}
