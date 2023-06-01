using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectKitt.Core.Game;
using ProjectKitt.Core.Services;
using ProjectKitt.WinForms.Forms;
using ProjectKitt.WinForms.Services;

namespace ProjectKitt.WinForms;

internal static class Program
{
   /// <summary>
   ///  The main entry point for the application.
   /// </summary>
   [STAThread]
   static void Main()
   {
      // To customize application configuration such as set high DPI settings or default font,
      // see https://aka.ms/applicationconfiguration.
      //ApplicationConfiguration.Initialize();

      var host = Host.CreateDefaultBuilder()
         .ConfigureServices((context, services) =>
         {
            ConfigureAutoMapper(services);
            ConfigureServices(context.Configuration, services);
         })
         .Build();

      var services = host.Services;
      Globals.ServiceProvider = services;
      var theForm = services.GetRequiredService<TestForm>();

      Application.Run(theForm);

      UserSettings.Default.Save();
   }

   private static void ConfigureAutoMapper(IServiceCollection services) =>
      services.AddAutoMapper(cfg => cfg.AddProfile(typeof(MappingProfile)));

   private static void ConfigureServices(IConfiguration configuration, IServiceCollection services) =>
      services
         .AddSingleton<MainForm>()
         .AddSingleton<TestForm>()
         
         .AddSingleton<IFactionLookup,FactionLookup>()
         .AddSingleton<IMapGridRepo, MapGridRepo>()
         .AddSingleton<ITheGame, TheGame>()
         .AddSingleton<ITheGameFactory, TheGameFactory>();
}
