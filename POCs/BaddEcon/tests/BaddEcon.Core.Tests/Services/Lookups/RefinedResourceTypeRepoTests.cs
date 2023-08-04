using BaddEcon.Core.Models;
using BaddEcon.Core.Services.Lookups;
using FluentAssertions;

namespace BaddEcon.Core.Tests.Services.Lookups;

public class RefinedResourceTypeLookupTests
{
   [Fact]
   public void GetAll_WillReturn()
   {
      var sut = new RefinedResourceTypeLookup();
      var result = sut.GetAll();
      result.Should().NotBeEmpty();
   }
   
   [Fact]
   public void Get_WillReturn()
   {
      var sut = new RefinedResourceTypeLookup();
      var result = sut.Get(LookupConstants.CopperIngot);
      result.Name.Should().Be("Copper Ingot");
      result.RawInputs.Should().ContainSingle();
   }
}
