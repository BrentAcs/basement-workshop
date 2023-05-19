﻿using ProjectKitt.Core.Models;

namespace ProjectKitt.WinForms.Services.Rendering;

public class MapGridStaticObjectRenderer : MapGridObjectRenderer, IMapGridObjectRenderer
{
   public override bool CanRender(IMapGridObject mapGridObject) => mapGridObject is IMapGridStaticObject;

   public override void Render(Graphics g, PointF viewPortOrigin)
   {
      if (MapGridObject is not IMapGridStaticObject staticObject)
         throw new InvalidOperationException();

      using var pen = new Pen(staticObject.PerimeterColor);
      using var brush = new SolidBrush(staticObject.PerimeterColor);

      var location = ScaleLocation(viewPortOrigin);

      var points = ComputeObjectsPoints(staticObject.PerimeterPoints, location).ToList();
      //var rotated = points.ToRotatePolygon(display, heading);

      points.ClosePolygon();
      g.DrawPolygon(pen, points.ToArray());
      //g.FillPolygon(brush, points.ToArray());
   }
}
