using Bass.Shared.Utilities;

namespace Bass.Shared.Tests.Utilities;

public class ValueRequestTests
{
   private readonly IRng _rng = RngMockHelper.GetRng();

   public enum TestEnum
   {
      First,
      Second,
      Third,
      Fourth,
      Fifth
   }
   
   // --- After construction

   [Fact]
   public void AfterCtor_WithAbsoluteValue_SubsetSelection_WillBeNull()
   {
      var sut = new ValueRequest<TestEnum>(TestEnum.First);

      sut.SubsetSelection.Should().BeNull();
   }

  
   [Fact]
   public void AfterCtor_WithSubsetSelection_UseAbsoluteValue_WillBeFalse()
   {
      var sut = new ValueRequest<TestEnum>( Enum.GetValues<TestEnum>());

      sut.UseAbsoluteValue.Should().BeFalse();
   }

   // --- After property set

   [Fact]
   public void After_AbsoluteValueSet_SubsetSelection_WillBeNull()
   {
      var sut = new ValueRequest<TestEnum>(TestEnum.First);

      sut.AbsoluteValue = TestEnum.Second;
   
      sut.SubsetSelection.Should().BeNull();
   }
   
   [Fact]
   public void After_SubsetSelectionSet_UseAbsoluteValue_WillBeFalse()
   {
      var sut = new ValueRequest<TestEnum>(TestEnum.First);

      sut.SubsetSelection = new[] {TestEnum.First, TestEnum.Third};

      sut.UseAbsoluteValue.Should().BeFalse();
   }
   
   // --- GetValue

   [Fact]
   public void GetValue_WillReturn_AbsoluteValue_WhenSet()
   {
      var sut = new ValueRequest<TestEnum>(TestEnum.First);

      var value = sut.GetValue(_rng);

      value.Should().Be(TestEnum.First);
   }

   [Fact]
   public void GetValue_WillReturn_ValueInSubset_WhenSet()
   {
      var sut = new ValueRequest<TestEnum>(Enum.GetValues<TestEnum>());

      var value = sut.GetValue(_rng);

      value.Should().BeOneOf(Enum.GetValues<TestEnum>());
   }
   
   [Fact]
   public void GetValue_WillReturn_ValueInSubset_WhenSet_Strings()
   {
      var choices = new[] { "Connor", "Lucas", "Ry", "Abby"};
      var sut = new ValueRequest<string>(choices);

      var value = sut.GetValue(_rng);

      value.Should().BeOneOf(choices);
   }
}
