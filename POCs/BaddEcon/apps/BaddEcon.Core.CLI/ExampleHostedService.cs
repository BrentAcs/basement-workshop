using BaddEcon.Core.Infrastructure.Storage;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public sealed class ExampleHostedService : IHostedService
{
   private readonly ILogger _logger;
   private readonly IRawResourceTypeRepo _rawResourceTypeRepo;
   private readonly IRefinedResourceTypeRepo _refinedResourceTypeRepo;

   public ExampleHostedService(
      ILogger<ExampleHostedService> logger,
      IHostApplicationLifetime appLifetime,
      IRawResourceTypeRepo rawResourceTypeRepo,
      IRefinedResourceTypeRepo refinedResourceTypeRepo)
   {
      _logger = logger;
      _rawResourceTypeRepo = rawResourceTypeRepo;
      _refinedResourceTypeRepo = refinedResourceTypeRepo;

      appLifetime.ApplicationStarted.Register(OnStarted);
      appLifetime.ApplicationStopping.Register(OnStopping);
      appLifetime.ApplicationStopped.Register(OnStopped);
   }

   public Task StartAsync(CancellationToken cancellationToken)
   {
      _logger.LogInformation("1. StartAsync has been called.");

      return Task.CompletedTask;
   }

   public Task StopAsync(CancellationToken cancellationToken)
   {
      _logger.LogInformation("4. StopAsync has been called.");

      return Task.CompletedTask;
   }

   private void OnStarted()
   {
      _logger.LogInformation("2. OnStarted has been called.");

      Task.Run(async () =>
      {
         try
         {
            await _rawResourceTypeRepo.SeedDataAsync().ConfigureAwait(false);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex);            
         }
         try
         {
            await _refinedResourceTypeRepo.SeedDataAsync().ConfigureAwait(false);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex);            
         }
      });
   }

   private void OnStopping()
   {
      _logger.LogInformation("3. OnStopping has been called.");
   }

   private void OnStopped()
   {
      _logger.LogInformation("5. OnStopped has been called.");
   }
}
