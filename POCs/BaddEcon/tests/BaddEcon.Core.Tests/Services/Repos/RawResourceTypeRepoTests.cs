using BaddEcon.Core.Models;
using BaddEcon.Core.Services.Repos;
using FluentAssertions;

namespace BaddEcon.Core.Tests.Services.Repos;

public class RawResourceTypeRepoTests
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
      var result = sut.Get(RawResource.CopperOre);
      result.Name.Should().Be("Copper Ore");
   }
}
