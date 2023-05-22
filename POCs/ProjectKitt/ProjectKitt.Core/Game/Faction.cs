namespace ProjectKitt.Core.Game;

public enum FactionAffinity
{
   Unknown,
   Neutral = 1,
   Peaceful,
   Hostile,
}

public interface IFaction
{
   const string Nato = "Nato";
   const string Pact = "Pact";
   const string Neutral = "Neutral";

   string Name { get; }
   string Description { get; }
   Color Color { get; }
}

public class Faction : IFaction
{
   public static IFaction Default => new Faction
   {
      Name = IFaction.Neutral,
      Color = Color.Gray
   }; 
   
   public string Name { get; set; } = string.Empty;
   public string Description { get; set; } = string.Empty;
   public Color Color { get; set; } = Color.Gray;
}

