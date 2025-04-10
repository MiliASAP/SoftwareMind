using Microsoft.EntityFrameworkCore;

namespace WebAPI_SoftwareMind.Services
{
    public class ExpiredNegotiationCleanerService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory; 
        private readonly ILogger<ExpiredNegotiationCleanerService> _logger;

        public ExpiredNegotiationCleanerService(IServiceScopeFactory serviceScopeFactory, ILogger<ExpiredNegotiationCleanerService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await RemoveExpiredNegotiationsAsync(stoppingToken); 
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while cleaning expired negotiations.");
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); 
            }
        }

        public async Task RemoveExpiredNegotiationsAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope()) 
            {
                var context = scope.ServiceProvider.GetRequiredService<NegotiationDbContext>(); 

                var expiredNegotiations = await context.Negotiations
                    .Where(n => n.Status != "Pending" && n.ExpirationDate <= DateTime.UtcNow)
                    .ToListAsync(stoppingToken);

                if (expiredNegotiations.Any())
                {
                    _logger.LogInformation($"{expiredNegotiations.Count} negotiation{(expiredNegotiations.Count > 1 ? "s" : "")} ha{(expiredNegotiations.Count > 1 ? "ve" : "s")} been deleted.");

                    context.Negotiations.RemoveRange(expiredNegotiations);
                    await context.SaveChangesAsync(stoppingToken); 
                }
                else
                {
                    _logger.LogInformation("No expired negotiations to remove.");
                }
            }
        }
    }
}
