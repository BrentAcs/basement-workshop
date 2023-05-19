using ProjectKitt.Core.Models;

namespace ProjectKitt.WinForms.Services.Rendering;

public interface IMapGridObjectRendererFactory
{
   IMapGridObjectRenderer? GetRenderer(IMapGridObject? mapGridObject);
}

public class MapGridObjectRendererFactory : IMapGridObjectRendererFactory
{
   private readonly IEnumerable<IMapGridObjectRenderer> _renders = new List<IMapGridObjectRenderer>
   {
      new MapGridStaticObjectRenderer(),
      new MapGridUnitObjectRenderer()
   };

   public IMapGridObjectRenderer? GetRenderer(IMapGridObject? mapGridObject)
   {
      if (mapGridObject == null)
         return null;

      return _renders.FirstOrDefault(renderer => renderer.CanRender(mapGridObject));
   }
}
