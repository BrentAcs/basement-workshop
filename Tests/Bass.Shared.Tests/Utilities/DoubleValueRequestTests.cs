using Bass.Shared.Utilities;

namespace Bass.Shared.Tests.Utilities;

public class DoubleValueRequestTests
{
   private readonly IRng _rng = RngMockHelper.GetRng();
  
   // --- After construction

   [Fact]
   public void AfterCtor_WithAbsoluteValue_SubsetSelection_WillBeNull()
   {
      var sut = new DoubleValueRequest(42d);

      sut.SubsetSelection.Should().BeNull();
   }

   [Fact]
   public void AfterCtor_WithAbsoluteValue_MinMaxSelection_WillBeNull()
   {
      var sut = new DoubleValueRequest(42d);

      sut.MinMaxSelection.Should().BeNull();
   }
   
   [Fact]
   public void AfterCtor_WithSubsetSelection_AbsoluteValue_WillBeNull()
   {
      var sut = new DoubleValueRequest(new []{42d,59d});
      
      sut.AbsoluteValue.Should().BeNull();
   }
   
   [Fact]
   public void AfterCtor_WithSubsetSelection_MinMaxSelection_WillBeNull()
   {
      var sut = new DoubleValueRequest(new []{42d,59d});

      sut.MinMaxSelection.Should().BeNull();
   }
   
   [Fact]
   public void AfterCtor_WithMinMaxSelection_AbsoluteValue_WillBeNull()
   {
      var sut = new DoubleValueRequest(new MinMax<double>(1d,2d));
      
      sut.AbsoluteValue.Should().BeNull();
   }
   
   [Fact]
   public void AfterCtor_WithMinMaxSelection_SubSetSelection_WillBeNull()
   {
      var sut = new DoubleValueRequest(new MinMax<double>(1d,2d));

      sut.SubsetSelection.Should().BeNull();
   }
   
   // --- After property set

   [Fact]
   public void After_AbsoluteValueSet_SubsetSelection_WillBeNull()
   {
      var sut = new DoubleValueRequest();

      sut.AbsoluteValue = 42d;
   
      sut.SubsetSelection.Should().BeNull();
   }
   
   [Fact]
   public void After_AbsoluteValueSet_MinMaxSelection_WillBeNull()
   {
      var sut = new DoubleValueRequest();
      
      sut.AbsoluteValue = 42d;
   
      sut.MinMaxSelection.Should().BeNull();
   }
   
   [Fact]
   public void After_SubsetSelectionSet_AbsoluteValue_WillBeNull()
   {
      var sut = new DoubleValueRequest();

      sut.SubsetSelection = new[] {42d, 59d};
      
      sut.AbsoluteValue.Should().BeNull();
   }
   
   [Fact]
   public void After_SubsetSelectionSet_MinMaxSelection_WillBeNull()
   {
      var sut = new DoubleValueRequest();

      sut.SubsetSelection = new[] {42d, 59d};
   
      sut.MinMaxSelection.Should().BeNull();
   }
   
   [Fact]
   public void After_MinMaxSelectionSet_AbsoluteValue_WillBeNull()
   {
      var sut = new DoubleValueRequest();

      sut.MinMaxSelection = new MinMax<double>(1d, 2d);
      
      sut.AbsoluteValue.Should().BeNull();
   }
   
   [Fact]
   public void After_MinMaxSelectionSet_SubSetSelection_WillBeNull()
   {
      var sut = new DoubleValueRequest();

      sut.MinMaxSelection = new MinMax<double>(1d, 2d);
   
      sut.SubsetSelection.Should().BeNull();
   }
   
   // --- GetValue

   [Fact]
   public void GetValue_WillReturn_AbsoluteValue_WhenSet()
   {
      var sut = new DoubleValueRequest(42d);

      var value = sut.GetValue(_rng);

      value.Should().Be(42d);
   }

   [Fact]
   public void GetValue_WillReturn_IntInSubset_WhenSet()
   {
      var sut = new DoubleValueRequest(new []{42d,69d,128d});

      var value = sut.GetValue(_rng);

      value.Should().BeOneOf(new[] {42d, 69d, 128d});
   }

   [Fact]
   public void GetValue_WillReturn_IntInMinMax_WhenSet()
   {
      var sut = new DoubleValueRequest(new MinMax<double>(1d,5d));

      var value = sut.GetValue(_rng);

      value.Should().BeInRange(1d, 5d);
   }
}
