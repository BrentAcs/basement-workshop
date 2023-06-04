using System.Drawing;
using ProjectKitt.Core.Extensions;
using ProjectKitt.Core.Game;
using ProjectKitt.Core.Models;
using ProjectKitt.WinForms.Extensions;

namespace ProjectKitt.Core.Tests.Extensions;

public class PointFExtensionsTests
{
   [Fact]
   public void CollisionTest_One_Yes()
   {
      var unit1 = new MapGridUnitObject
      {
         Location = new PointF(7500, 7500),
         UnitType = UnitType.Armor,
         UnitSize = UnitSize.Division,
         Orientation = 90f,
         AreaOfControlRadius = 4000,
      };
      var unit2 = new MapGridUnitObject
      {
         Location = new PointF(10500, 7500),
         UnitType = UnitType.Armor,
         UnitSize = UnitSize.Division,
         Orientation = 290f,
         AreaOfControlRadius = 4000, 
      };

      var unit1ZoC = unit1.Location.ComputePointsAtRadius(unit1.AreaOfControlRadius);
      var unit2ZoC = unit2.Location.ComputePointsAtRadius(unit2.AreaOfControlRadius);

      bool is1Inside2 = unit1ZoC.IsInsideCircle(unit2.AreaOfControlRadius);
      bool is2Inside1 = unit2ZoC.IsInsideCircle(unit1.AreaOfControlRadius);

      is1Inside2.Should().BeTrue();
      is2Inside1.Should().BeTrue();
   }
   
   [Fact]
   public void CollisionTest_Two_No()
   {
      var unit1 = new MapGridUnitObject
      {
         Location = new PointF(7500,12500),
         UnitType = UnitType.Armor,
         UnitSize = UnitSize.Division,
         Orientation = 90f,
         AreaOfControlRadius = 4000,
      };
      var unit2 = new MapGridUnitObject
      {
         Location = new PointF(12500,12500),
         UnitType = UnitType.Armor,
         UnitSize = UnitSize.Division,
         Orientation = 290f,
         AreaOfControlRadius = 4000, 
      };

      var unit1ZoC = unit1.Location.ComputePointsAtRadius(unit1.AreaOfControlRadius);
      var unit2ZoC = unit2.Location.ComputePointsAtRadius(unit2.AreaOfControlRadius);

      bool is1Inside2 = unit1ZoC.IsInsideCircle(unit2.AreaOfControlRadius);
      bool is2Inside1 = unit2ZoC.IsInsideCircle(unit1.AreaOfControlRadius);

      is1Inside2.Should().BeFalse();
      is2Inside1.Should().BeFalse();
   }

   [Fact]
   public void CollisionTest_Three_No()
   {
      var unit1 = new MapGridUnitObject
      {
         Location = new PointF(7500,20000),
         UnitType = UnitType.Armor,
         UnitSize = UnitSize.Division,
         Orientation = 90f,
         AreaOfControlRadius = 4000,
      };
      var unit2 = new MapGridUnitObject
      {
         Location = new PointF(11500,20000),
         UnitType = UnitType.Armor,
         UnitSize = UnitSize.Division,
         Orientation = 290f,
         AreaOfControlRadius = 4000, 
      };

      var unit1ZoC = unit1.Location.ComputePointsAtRadius(unit1.AreaOfControlRadius);
      var unit2ZoC = unit2.Location.ComputePointsAtRadius(unit2.AreaOfControlRadius);

      bool is1Inside2 = unit1ZoC.IsInsideCircle(unit2.AreaOfControlRadius);
      bool is2Inside1 = unit2ZoC.IsInsideCircle(unit1.AreaOfControlRadius);

      is1Inside2.Should().BeFalse();
      is2Inside1.Should().BeFalse();
   }

   
}
