namespace Bass.Shared.Geometry;

public record Ellipsoid(int X, int Y, int Z)

{
   public int X { get; set; } = X;
   public int Y { get; set; } = Y;
   public int Z { get; set; } = Z;

   public static readonly Ellipsoid Empty = new(default, default, default);

   public void Inflate(int value) => Inflate(value, value, value);
   
   public void Inflate(int x, int y, int z)
   {
      X += x;
      Y += y;
      Z += z;
   } 
}
