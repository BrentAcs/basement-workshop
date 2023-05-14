namespace ProjectKitt.WinForms.Services;

public interface IRenderedObject
{
   IEnumerable<PointF> Points { get; }
}

public abstract class RenderedObject : IRenderedObject
{
   public abstract IEnumerable<PointF> Points { get; }
}

public class TestRenderedObject : RenderedObject
{
   private static IList<PointF> _points = new List<PointF>
   {
      new(-4, -12),
      new(4, -12),
      new(4, 12),
      new(-4, 12),
   };

   public override IEnumerable<PointF> Points => _points;
}

public class _10RenderedObject : RenderedObject
{
   private static IList<PointF> _points = new List<PointF>
   {
      new(-10, -10),
      new(10, -10),
      new(10, 10),
      new(-10, 10),
   };

   public override IEnumerable<PointF> Points => _points;
}

public class _100RenderedObject : RenderedObject
{
   private static IList<PointF> _points = new List<PointF>
   {
      new(-100, -100),
      new(100, -100),
      new(100, 100),
      new(-100, 100),
   };

   public override IEnumerable<PointF> Points => _points;
}

public class _200RenderedObject : RenderedObject
{
   private static IList<PointF> _points = new List<PointF>
   {
      new(-200, -200),
      new(200, -200),
      new(200, 200),
      new(-200, 200),
   };

   public override IEnumerable<PointF> Points => _points;
}

public class _1000RenderedObject : RenderedObject
{
   private static IList<PointF> _points = new List<PointF>
   {
      new(-1000, -1000),
      new(1000, -1000),
      new(1000, 1000),
      new(-1000, 1000),
   };

   public override IEnumerable<PointF> Points => _points;
}

