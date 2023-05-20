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
   public string Name { get; set; } = string.Empty;
   public string Description { get; set; } = string.Empty;
   public Color Color { get; set; } = Color.Gray;
}

public interface IFactionCollection
{
   IEnumerable<IFaction> All { get; }
   void Add(IFaction faction);
   void Reset();
   
   void ResetAffinities();

   void SetAffinity(string source, string target, FactionAffinity affinity);
   void SetAffinity(IFaction source, IFaction target, FactionAffinity affinity);
   
   FactionAffinity GetAffinity(string source, string target);
   FactionAffinity GetAffinity(IFaction source, IFaction target);
}

public class FactionCollection : IFactionCollection, IEqualityComparer<(string, string)>
{
   private readonly IDictionary<string, IFaction> _factions;
   private readonly IDictionary<(string, string), FactionAffinity> _affinities;
   
   public FactionCollection()
   {
      _factions = new Dictionary<string, IFaction>(StringComparer.OrdinalIgnoreCase);
      _affinities = new Dictionary<(string, string), FactionAffinity>(this);
      Reset();
   }

   public IEnumerable<IFaction> All => _factions.Values;

   public void Reset()
   {
      _factions.Clear();

      Add(new Faction
      {
         Name = IFaction.Nato,
         Description = "North Atlantic Treaty Organization",
         Color = Color.DodgerBlue
      });
      Add(new Faction
      {
         Name = IFaction.Pact,
         Description = "Warsaw Pact",
         Color = Color.DarkRed
      });
      Add(new Faction
      {
         Name = IFaction.Neutral,
         Description = "Neutral Paties",
         Color = Color.Gray
      });
      
      ResetAffinities();
   }
   
   public void ResetAffinities()
   {
      SetAffinity(IFaction.Nato, IFaction.Pact, FactionAffinity.Hostile);
      SetAffinity(IFaction.Nato, IFaction.Neutral, FactionAffinity.Neutral);
      SetAffinity(IFaction.Pact, IFaction.Nato, FactionAffinity.Hostile);
      SetAffinity(IFaction.Pact, IFaction.Neutral, FactionAffinity.Neutral);
   }

   public void Add(IFaction faction) => _factions.Add(faction.Name, faction);

   public void SetAffinity(string source, string target, FactionAffinity affinity) =>
      SetAffinity(_factions[ source ], _factions[ target ], affinity);

   public void SetAffinity(IFaction source, IFaction target, FactionAffinity affinity) =>
      _affinities.TryAdd((source.Name, target.Name), affinity);

   public FactionAffinity GetAffinity(string source, string target) =>
      _affinities.TryGetValue((source, target), out var affinity) ? affinity : FactionAffinity.Unknown;

   public FactionAffinity GetAffinity(IFaction source, IFaction target) =>
      GetAffinity(source.Name, target.Name);

   public bool Equals((string, string) x, (string, string) y) =>
      x.Item1.Equals(y.Item1, StringComparison.OrdinalIgnoreCase) && x.Item2.Equals(y.Item2, StringComparison.OrdinalIgnoreCase);

   public int GetHashCode((string, string) obj) => HashCode.Combine(obj.Item1, obj.Item2);
}