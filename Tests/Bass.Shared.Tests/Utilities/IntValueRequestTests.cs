using Bass.Shared.Utilities;

namespace Bass.Shared.Tests.Utilities;

public class IntValueRequestTests
{
   private readonly IRng _rng = RngMockHelper.GetRng();
   
   // --- After construction

   [Fact]
   public void AfterCtor_WithAbsoluteValue_SubsetSelection_WillBeNull()
   {
      var sut = new IntValueRequest(42);

      sut.SubsetSelection.Should().BeNull();
   }

   [Fact]
   public void AfterCtor_WithAbsoluteValue_MinMaxSelection_WillBeNull()
   {
      var sut = new IntValueRequest(42);

      sut.MinMaxSelection.Should().BeNull();
   }
   
   [Fact]
   public void AfterCtor_WithSubsetSelection_AbsoluteValue_WillBeNull()
   {
      var sut = new IntValueRequest(new []{42,59});
      
      sut.AbsoluteValue.Should().BeNull();
   }
   
   [Fact]
   public void AfterCtor_WithSubsetSelection_MinMaxSelection_WillBeNull()
   {
      var sut = new IntValueRequest(new []{42,59});

      sut.MinMaxSelection.Should().BeNull();
   }
   
   [Fact]
   public void AfterCtor_WithMinMaxSelection_AbsoluteValue_WillBeNull()
   {
      var sut = new IntValueRequest(new MinMax<int>(1,2));
      
      sut.AbsoluteValue.Should().BeNull();
   }
   
   [Fact]
   public void AfterCtor_WithMinMaxSelection_SubSetSelection_WillBeNull()
   {
      var sut = new IntValueRequest(new MinMax<int>(1,2));

      sut.SubsetSelection.Should().BeNull();
   }
   
   // --- After property set

   [Fact]
   public void After_AbsoluteValueSet_SubsetSelection_WillBeNull()
   {
      var sut = new IntValueRequest();

      sut.AbsoluteValue = 42;
   
      sut.SubsetSelection.Should().BeNull();
   }
   
   [Fact]
   public void After_AbsoluteValueSet_MinMaxSelection_WillBeNull()
   {
      var sut = new IntValueRequest();
      
      sut.AbsoluteValue = 42;
   
      sut.MinMaxSelection.Should().BeNull();
   }
   
   [Fact]
   public void After_SubsetSelectionSet_AbsoluteValue_WillBeNull()
   {
      var sut = new IntValueRequest();

      sut.SubsetSelection = new[] {42, 59};
      
      sut.AbsoluteValue.Should().BeNull();
   }
   
   [Fact]
   public void After_SubsetSelectionSet_MinMaxSelection_WillBeNull()
   {
      var sut = new IntValueRequest();

      sut.SubsetSelection = new[] {42, 59};
   
      sut.MinMaxSelection.Should().BeNull();
   }
   
   [Fact]
   public void After_MinMaxSelectionSet_AbsoluteValue_WillBeNull()
   {
      var sut = new IntValueRequest();

      sut.MinMaxSelection = new MinMax<int>(1, 2);
      
      sut.AbsoluteValue.Should().BeNull();
   }
   
   [Fact]
   public void After_MinMaxSelectionSet_SubSetSelection_WillBeNull()
   {
      var sut = new IntValueRequest();

      sut.MinMaxSelection = new MinMax<int>(1, 2);
   
      sut.SubsetSelection.Should().BeNull();
   }
   
   // --- GetValue

   [Fact]
   public void GetValue_WillReturn_AbsoluteValue_WhenSet()
   {
      var sut = new IntValueRequest(42);

      var value = sut.GetValue(_rng);

      value.Should().Be(42);
   }

   [Fact]
   public void GetValue_WillReturn_IntInSubset_WhenSet()
   {
      var sut = new IntValueRequest(new []{42,69,128});

      var value = sut.GetValue(_rng);

      value.Should().BeOneOf(new[] {42, 69, 128});
   }

   [Fact]
   public void GetValue_WillReturn_IntInMinMax_WhenSet()
   {
      var sut = new IntValueRequest(new MinMax<int>(1,5));

      var value = sut.GetValue(_rng);

      value.Should().BeInRange(1, 5);
   }
}
