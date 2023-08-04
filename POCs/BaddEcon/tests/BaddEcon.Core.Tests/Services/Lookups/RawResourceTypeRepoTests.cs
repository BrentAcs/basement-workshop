using BaddEcon.Core.Models;
using BaddEcon.Core.Services.Lookups;
using FluentAssertions;

namespace BaddEcon.Core.Tests.Services.Lookups;

public class RawResourceTypeLookupTests
{
   [Fact]
   public void GetAll_WillReturn()
   {
      var sut = new RawResourceTypeLookup();
      var result = sut.GetAll();
      result.Should().NotBeEmpty();
   }
   
   [Fact]
   public void Get_WillReturn()
   {
      var sut = new RawResourceTypeLookup();
      var result = sut.Get(LookupConstants.CopperOre);
      result.Name.Should().Be("Copper Ore");
   }
}
