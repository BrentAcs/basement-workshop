namespace Turncoats.Game;

public interface IStonesForZone : IStoneContainer
{
   Stone GetVictor();
}

public class StonesForZone : StoneContainer, IStonesForZone
{
   protected override IDictionary<Stone, int> InitialState => new Dictionary<Stone, int>
   {
      {Stone.Red, 0},
      {Stone.Blue, 0},
      {Stone.Black, 0},
   };
   
   public Stone GetVictor()
   {
      var ordered = _quantities.OrderBy(_ => _.Value)
         .Reverse()
         .ToList();
      return ordered[0].Value == ordered[1].Value ? Stone.None : ordered[0].Key;
   }
}
