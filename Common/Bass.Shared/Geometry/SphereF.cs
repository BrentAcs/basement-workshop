namespace Bass.Shared.Geometry;

public record SphereF : EllipsoidF
{
   public SphereF(double radius)
      : base(radius, radius, radius)
   {
      
   }
}
