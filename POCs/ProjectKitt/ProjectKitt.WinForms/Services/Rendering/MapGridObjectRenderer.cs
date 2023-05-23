using ProjectKitt.Core.Extensions;
using ProjectKitt.Core.Models;

namespace ProjectKitt.WinForms.Services.Rendering;

public interface IMapGridObjectRenderer
{
   bool CanRender(IMapGridObject mapGridObject);
   IMapGridObjectRenderer Initialize(IMapGridObject mapGridObject, ScaleFactor scaleFactor, PointF viewPortOrigin);
   void Render(Graphics g, PointF viewPortOrigin);
}

public abstract class MapGridObjectRenderer : IMapGridObjectRenderer
{
   protected IMapGridObject? MapGridObject { get; private set; }
   protected PointF ViewPortOrigin { get; private set; }
   protected PointF ScaledViewPortOrigin { get; private set; }
   protected ScaleFactor ScaleFactor { get; private set; }
   protected float ScaleFactorValue { get; private set; }

   public abstract bool CanRender(IMapGridObject mapGridObject);

   public IMapGridObjectRenderer Initialize(IMapGridObject mapGridObject, ScaleFactor scaleFactor, PointF viewPortOrigin)
   {
      System.Diagnostics.Debug.WriteLine($"{nameof(Initialize)}: viewPortOrigin: {viewPortOrigin}");

      MapGridObject = mapGridObject;
      ScaleFactor = scaleFactor;
      ScaleFactorValue = ScaleFactor.GetScaleFactorValue();
      ViewPortOrigin = viewPortOrigin; 
      ScaledViewPortOrigin = viewPortOrigin.ScaleBy(ScaleFactorValue);

      return this;
   }
   public abstract void Render(Graphics g, PointF viewPortOrigin);

   protected PointF ScaleLocation(PointF viewPortOrigin) => new(
      MapGridObject!.Location.X.ScaleBy(ScaleFactorValue) - viewPortOrigin.X,
      MapGridObject.Location.Y.ScaleBy(ScaleFactorValue) - viewPortOrigin.Y);

   protected IEnumerable<PointF> ComputeObjectsPoints(IEnumerable<PointF> objectPoints, PointF center)
   {
      var scaleFactor = ScaleFactor.GetScaleFactorValue();
      var result = new List<PointF>();
      foreach (var point in objectPoints)
      {
         result.Add(new PointF(center.X + point.X * scaleFactor, center.Y + point.Y * scaleFactor));
      }

      return result;
   }
}
