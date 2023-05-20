using System.Drawing;
using ProjectKitt.Core.Game;

namespace ProjectKitt.Core.Tests._Rnd;

public class FactionAffinityMapperTests
{
   private static IFactionCollection FactionCollection => new FactionCollection();
   
   // [Fact]
   // public void All_ShouldContain_Three()
   // {
   //    var sut = FactionAffinityMapper;
   //    sut.All.Should().HaveCount(3);
   // }
   
   [Fact]
   public void GetAffinity_Nato_To_Neutral_WillBeNeutral()
   {
      var sut = FactionCollection;
      var result = sut.GetAffinity(IFaction.Nato, IFaction.Neutral);
      result.Should().Be(FactionAffinity.Neutral);
   }

   [Theory]
   [InlineData(IFaction.Nato, IFaction.Neutral, FactionAffinity.Neutral)]
   [InlineData(IFaction.Pact, IFaction.Neutral, FactionAffinity.Neutral)]
   [InlineData(IFaction.Nato, IFaction.Pact, FactionAffinity.Hostile)]
   [InlineData(IFaction.Pact, IFaction.Nato, FactionAffinity.Hostile)]
   public void GetAffinity_Theories(string source, string target, FactionAffinity expected)
   {
      var sut = FactionCollection;
      var result = sut.GetAffinity(source, target);
      result.Should().Be(expected);
   }
}
