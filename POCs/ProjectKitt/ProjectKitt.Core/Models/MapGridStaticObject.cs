namespace ProjectKitt.Core.Models;

public interface IMapGridStaticObject : IMapGridObject
{
   IEnumerable<PointF> PerimeterPoints { get; }
   Color PerimeterColor { get; }
}

public class MapGridStaticObject : MapGridObject, IMapGridStaticObject
{
   public IEnumerable<PointF> PerimeterPoints { get; set; } = new[] { PointF.Empty };
   public Color PerimeterColor { get; set; } = Color.Yellow;
}
