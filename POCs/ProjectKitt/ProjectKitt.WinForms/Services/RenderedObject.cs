namespace ProjectKitt.WinForms.Services;

public interface IRenderedObject
{
   IEnumerable<PointF> Points { get; }
   IEnumerable<IEnumerable<PointF>> ComplexPoints { get; }
}

public abstract class RenderedObject : IRenderedObject
{
   public abstract IEnumerable<PointF> Points { get; }
   public abstract IEnumerable<IEnumerable<PointF>> ComplexPoints { get; }
}

public class TestRenderedObject : RenderedObject
{
   // Nimitz carrier: 332m x 76m  ()
   private static IList<PointF> _points = new List<PointF>
   {
      new(-38, -166),
      new(38, -166),
      new(38, 166),
      new(-38, 166),
   };

   // private static IEnumerable<PointF>[] _complexPoints = new []
   // {
   //    new List<PointF>
   //    {
   //       new(-38, -166),
   //       new(38, -166),
   //       new(38, 166),
   //       new(-38, 166),
   //    },
   //    new List<PointF>
   //    {
   //       new(-10, -10),
   //       new(10, -10),
   //       new(10, 10),
   //       new(-10, 10),
   //    }      
   // }; 

   private static List<List<PointF>> _complexPoints = new List<List<PointF>>();

   public TestRenderedObject()
   {
      AddRect(76, 332);
      AddRect(50, 100);
      AddRect(10, 50);
   }

   private void AddRect(float width, float height)
   {
      _complexPoints.Add(new List<PointF>
      {
         new(-(width/2), -(height/2)),
         new((width/2), -(height/2)),
         new((width/2), (height/2)),
         new(-(width/2), (height/2)),
      });
   }

   public override IEnumerable<PointF> Points => _points;
   public override IEnumerable<IEnumerable<PointF>> ComplexPoints => _complexPoints;
}

// public class _10RenderedObject : RenderedObject
// {
//    private static IList<PointF> _points = new List<PointF>
//    {
//       new(-10, -10),
//       new(10, -10),
//       new(10, 10),
//       new(-10, 10),
//    };
//
//    public override IEnumerable<PointF> Points => _points;
// }
//
// public class _100RenderedObject : RenderedObject
// {
//    private static IList<PointF> _points = new List<PointF>
//    {
//       new(-100, -100),
//       new(100, -100),
//       new(100, 100),
//       new(-100, 100),
//    };
//
//    public override IEnumerable<PointF> Points => _points;
// }
//
// public class _200RenderedObject : RenderedObject
// {
//    private static IList<PointF> _points = new List<PointF>
//    {
//       new(-200, -200),
//       new(200, -200),
//       new(200, 200),
//       new(-200, 200),
//    };
//
//    public override IEnumerable<PointF> Points => _points;
// }
//
// public class _1000RenderedObject : RenderedObject
// {
//    private static IList<PointF> _points = new List<PointF>
//    {
//       new(-1000, -1000),
//       new(1000, -1000),
//       new(1000, 1000),
//       new(-1000, 1000),
//    };
//
//    public override IEnumerable<PointF> Points => _points;
// }
