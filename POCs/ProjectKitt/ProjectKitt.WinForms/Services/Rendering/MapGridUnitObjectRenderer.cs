using System.Drawing.Drawing2D;
using Bass.Shared.Extensions;
using ProjectKitt.Core.Extensions;
using ProjectKitt.Core.Models;
using ProjectKitt.WinForms.Extensions;

namespace ProjectKitt.WinForms.Services.Rendering;

public class MapGridUnitObjectRenderer : MapGridObjectRenderer, IMapGridObjectRenderer
{
   public override bool CanRender(IMapGridObject mapGridObject) => mapGridObject is IMapGridUnitObject;

   private static readonly RectangleF _overallRect = new(PointF.Empty, new SizeF(56, 56));
   private static readonly RectangleF _symbolRect = new(PointF.Empty, new SizeF(45, 30));

   private IMapGridUnitObject UnitObject
   {
      get
      {
         if (MapGridObject is not IMapGridUnitObject unitObject)
            throw new InvalidOperationException();
         return unitObject;
      }
   }

   public override void Render(Graphics g)
   {
      using var pen = new Pen(Color.White, 2);
      var location = ScaleLocation(ViewPortOrigin);
      var symbolRect = _symbolRect.CenterOn(location);

      DrawAreaOfControl(g, location, UnitObject.Owner.Color);
      g.DrawRectangle(pen, symbolRect);

      DrawUnitSymbol(g, pen, symbolRect);
      DrawOrientationIndicator(g, location, Color.DeepSkyBlue);
   }

   private void DrawUnitSymbol(Graphics g, Pen pen, RectangleF symbolRect)
   {
      switch (UnitObject.UnitType)
      {
         case UnitType.Armor:
            DrawArmorUnitSymbol(g, pen, symbolRect);
            break;
         case UnitType.Infantry:
            DrawInfantryUnitSymbol(g, pen, symbolRect);
            break;
         case UnitType.MechInfantry:
            DrawArmorUnitSymbol(g, pen, symbolRect);
            DrawInfantryUnitSymbol(g, pen, symbolRect);
            break;
         //default:
         //   throw new ArgumentOutOfRangeException();
      }
   }

   private void DrawArmorUnitSymbol(Graphics g, Pen pen, RectangleF symbolRect)
   {
      var rect = symbolRect.Deflate(10, 15);
      g.DrawEllipse(pen, rect);
   }

   private void DrawInfantryUnitSymbol(Graphics g, Pen pen, RectangleF symbolRect)
   {
      g.DrawLine(pen,
         symbolRect.Location,
         new PointF(symbolRect.Location.X + symbolRect.Width, symbolRect.Location.Y + symbolRect.Height));
      g.DrawLine(pen,
         symbolRect.Location with {Y = symbolRect.Location.Y + symbolRect.Height},
         symbolRect.Location with {X = symbolRect.Location.X + symbolRect.Width});
   }

   private void DrawOrientationIndicator(Graphics g, PointF location, Color color)
   {
      if (MapGridObject is not IMapGridUnitObject unitObject)
         throw new InvalidOperationException();

      const float facingMarkerLength = 35;
      const float arrowHeadAngle = 135;
      const float arrowHeadLength = 7;

      using var facingPen = new Pen(color, 2);

      var rayEnd = location.ComputeRayEndPoint(unitObject.Orientation, facingMarkerLength);
      g.DrawLine(facingPen, location, rayEnd);

      var arrowHeadLeft = rayEnd.ComputeRayEndPoint(unitObject.Orientation - arrowHeadAngle, arrowHeadLength);
      g.DrawLine(facingPen, rayEnd, arrowHeadLeft);

      var arrowHeadRight = rayEnd.ComputeRayEndPoint(unitObject.Orientation + arrowHeadAngle, arrowHeadLength);
      g.DrawLine(facingPen, rayEnd, arrowHeadRight);
   }

   private void DrawAreaOfControl(Graphics g, PointF location, Color color)
   {
      color = ChangeColorBrightness(color, .5f);
      if (Settings!.ShowAreaOfControl)
      {
         using var brush = new SolidBrush(color);
         
         var aocRadius = UnitObject.AreaOfControlRadius.ScaleBy(ScaleFactorValue);
         var aocCircle = new RectangleF(location.X - aocRadius, location.Y - aocRadius, aocRadius * 2, aocRadius * 2);
         g.FillEllipse(brush, aocCircle);
      }

      if (Settings!.ShowAreaOfControlPoints)
      {
         var points = UnitObject.Location.ComputePointsAtRadius(UnitObject.AreaOfControlRadius);

         var scaled = points.Select(point => point.ScaleBy(ScaleFactorValue));
         scaled = scaled.Offset(ViewPortOrigin).ToList();
         scaled = scaled.ClosePolygon();

         using var pen = new Pen(Color.White, 2);
         g.DrawPolygon(pen, scaled.ToArray());
      }
   }

   public static Color ChangeColorBrightness(Color color, float correctionFactor)
   {
      // https://stackoverflow.com/questions/801406/c-create-a-lighter-darker-color-based-on-a-system-color
      float red = color.R;
      float green = color.G;
      float blue = color.B;

      if (correctionFactor < 0)
      {
         correctionFactor = 1 + correctionFactor;
         red *= correctionFactor;
         green *= correctionFactor;
         blue *= correctionFactor;
      }
      else
      {
         red = (255 - red) * correctionFactor + red;
         green = (255 - green) * correctionFactor + green;
         blue = (255 - blue) * correctionFactor + blue;
      }

      return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
   }
}
