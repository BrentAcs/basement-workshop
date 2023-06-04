using ProjectKitt.Core.Extensions;
using ProjectKitt.Core.Models;

namespace ProjectKitt.WinForms.Services.Rendering;

public interface IMapGridObjectRenderer
{
   bool CanRender(IMapGridObject mapGridObject);
   IMapGridObjectRenderer Initialize(IMapGridObject mapGridObject, IMapGridObjectRendererSettings settings);
   void Render(Graphics g);
}

public interface IMapGridObjectRendererSettings
{
   ScaleFactor ScaleFactor { get; }
   PointF ViewPortOrigin { get; }
   bool ShowAreaOfControl { get;  }
   bool ShowAreaOfControlPoints { get;  }
} 

public abstract class MapGridObjectRenderer : IMapGridObjectRenderer
{
   protected IMapGridObject? MapGridObject { get; private set; }
   protected IMapGridObjectRendererSettings? Settings { get; private set; }

   protected PointF ViewPortOrigin => Settings!.ViewPortOrigin;
   protected PointF ScaledViewPortOrigin => ViewPortOrigin.ScaleBy(ScaleFactorValue);
   protected ScaleFactor ScaleFactor => Settings!.ScaleFactor;
   protected float ScaleFactorValue => ScaleFactor.GetScaleFactorValue();

   public abstract bool CanRender(IMapGridObject mapGridObject);

   public IMapGridObjectRenderer Initialize(IMapGridObject mapGridObject, IMapGridObjectRendererSettings settings)
   {
      MapGridObject = mapGridObject;
      Settings = settings;
      return this;
   }

   public abstract void Render(Graphics g /*, PointF viewPortOrigin*/);

   protected PointF ScaleLocation(PointF viewPortOrigin) => new(
      MapGridObject!.Location.X.ScaleBy(ScaleFactorValue) - viewPortOrigin.X,
      MapGridObject.Location.Y.ScaleBy(ScaleFactorValue) - viewPortOrigin.Y);

   protected IEnumerable<PointF> ComputeObjectsPoints(IEnumerable<PointF> objectPoints, PointF center) =>
      objectPoints.Select(point =>
         new PointF(center.X + point.X * ScaleFactorValue, center.Y + point.Y * ScaleFactorValue)).ToList();
}
