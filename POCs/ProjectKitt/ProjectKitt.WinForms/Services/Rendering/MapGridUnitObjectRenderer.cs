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

   public override void Render(Graphics g, PointF viewPortOrigin)
   {
      using var pen = new Pen(Color.White, 2);
      var location = ScaleLocation(viewPortOrigin);
      var symbolRect = _symbolRect.CenterOn(location);

      DrawZoneOfControl(g, location, Color.Red);
      g.DrawRectangle(pen, symbolRect);

      DrawUnitSymbol(g, pen, symbolRect);
      DrawFacingIndicator(g, location, Color.DeepSkyBlue);
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

   private void DrawFacingIndicator(Graphics g, PointF location, Color color)
   {
      if (MapGridObject is not IMapGridUnitObject unitObject)
         throw new InvalidOperationException();

      const float facingMarkerLength = 35;
      const float arrowHeadAngle = 135;
      const float arrowHeadLength = 7;

      using var facingPen = new Pen(color, 2);

      var rayEnd = location.ComputeRayEndPoint(unitObject.Facing, facingMarkerLength);
      g.DrawLine(facingPen, location, rayEnd);

      var arrowHeadLeft = rayEnd.ComputeRayEndPoint(unitObject.Facing - arrowHeadAngle, arrowHeadLength);
      g.DrawLine(facingPen, rayEnd, arrowHeadLeft);

      var arrowHeadRight = rayEnd.ComputeRayEndPoint(unitObject.Facing + arrowHeadAngle, arrowHeadLength);
      g.DrawLine(facingPen, rayEnd, arrowHeadRight);
   }

   private void DrawZoneOfControl(Graphics g, PointF location, Color color)
   {
      const float controlRadius = 2000;
      float[] angles = {330f, 30f, 90f, 150f, 210f, 270f};

      var points = angles
         .Select(angle => location.ComputeRayEndPoint(angle, controlRadius.ScaleBy(ScaleFactorValue))).ToList();

      points.ClosePolygon();
      var rotated = points.ToRotatePolygon(location, UnitObject.Facing);

      using var pen = new Pen(color, 1);
      g.DrawPolygon(pen, rotated.ToArray());

      using var brush = new SolidBrush(ChangeColorBrightness(color, .5f));
      g.FillPolygon(brush, rotated.ToArray());
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
