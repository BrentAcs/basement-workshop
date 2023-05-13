using Bass.Shared.Utilities;

namespace Bass.Shared.Tests.Utilities;

public class ValuesRequestTests
{
   private readonly IRng _rng = RngMockHelper.GetRng();

   // --- GetValues

   [Fact]
   public void GetValues_WillReturn_UniqueValuesInSubset_WhenSet()
   {
      var names = new[]{ "Connor", "Lucas", "Ry", "Abby"};
      
      var sut = new ValuesRequest<string>(names);
   
      var values = sut.GetValues(_rng, 2);
   
      values.Distinct().Should().HaveCount(2);
   }
}
