using Bass.Shared.Utilities;

namespace Bass.Shared.Tests.Utilities;

public static class RngMockHelper
{
   public static IRng GetRng()
   {
#if  true
      return new SimpleRng();
#else      
      return GetRngMock().Object;
#endif   
   }
   
   public static Mock<IRng> GetRngMock()
   {
      var rngMock = new Mock<IRng>();
      
      rngMock.Setup(x => x.Next(It.IsAny<int>()))
         .Returns<int>(max => max-1);
      rngMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>()))
         .Returns<int, int>((_, max) => max-1);
      rngMock.Setup(x => x.Next(It.IsAny<double>(), It.IsAny<double>()))
         .Returns<double, double>((_, max) => max-1);

      return rngMock;
   }    
}
