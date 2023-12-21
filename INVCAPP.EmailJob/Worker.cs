using Cronos;
using INVCAPP.Core.Services;

namespace INVCAPP.EmailJob
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEmailService _emailService;
        private readonly CronExpression _cronExpression;
        private DateTime _nextRun;
        private CancellationTokenSource _cancellationTokenSource;

        public Worker(ILogger<Worker> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
            _cronExpression = CronExpression.Parse("* * * * *"); // her dakika

            _nextRun = _cronExpression.GetNextOccurrence(DateTime.UtcNow).GetValueOrDefault();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                if (now > _nextRun)
                {
                    await _emailService.ProcessAndSendEmailForUnprocessedInvoices();
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    _nextRun = _cronExpression.GetNextOccurrence(DateTime.UtcNow).GetValueOrDefault();
                }
                await Task.Delay(5000, stoppingToken); // 5 saniyede bir kontrol et
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker is stopping.");
            _cancellationTokenSource.Cancel();
            await base.StopAsync(stoppingToken);
        }

        

    }
}
