using Microsoft.Extensions.DependencyInjection;
using ProjectKitt.Core.Game;
using ProjectKitt.Core.Services;

namespace ProjectKitt.WinForms;

public static class Globals
{
   public static IServiceProvider? ServiceProvider { get; set; }

   public static ITheGame TheGame => ServiceProvider!.GetRequiredService<ITheGame>();
   public static ITheGameFactory TheGameFactory => ServiceProvider!.GetRequiredService<ITheGameFactory>();
   public static IMapGridRepo MapGridRepo => ServiceProvider!.GetRequiredService<IMapGridRepo>();
}
