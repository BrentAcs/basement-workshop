using Bass.Shared.Extensions;

namespace Bass.Shared.Utilities;

public class IntValueRequest
{
   private int? _absoluteValue;
   private IEnumerable<int>? _subsetSelection;
   private MinMax<int>? _minMaxSelection;

   public IntValueRequest()
   {
   }

   public IntValueRequest(int absoluteValue)
   {
      _absoluteValue = absoluteValue;
   }

   public IntValueRequest(IEnumerable<int>? subsetSelection)
   {
      _subsetSelection = subsetSelection;
   }

   public IntValueRequest(int min, int max) : this(new MinMax<int>(min, max))
   {
   }
   
   public IntValueRequest(MinMax<int>? minMaxSelection)
   {
      _minMaxSelection = minMaxSelection;
   }

   public int? AbsoluteValue
   {
      get => _absoluteValue;
      set => SetAbsoluteValue(value);
   }

   public IEnumerable<int>? SubsetSelection
   {
      get => _subsetSelection;
      set => SetSubsetSelection(value);
   }

   public MinMax<int>? MinMaxSelection
   {
      get => _minMaxSelection;
      set => SetMinMaxSelection(value);
   }

   public int GetValue(IRng rng)
   {
      if (_absoluteValue is not null)
         return _absoluteValue.Value;

      if (_subsetSelection is not null)
         return rng.Next(_subsetSelection);
      
      if (_minMaxSelection is not null)
         return rng.Next(_minMaxSelection);

      throw new NotImplementedException();
   }
   
   private void SetAbsoluteValue(int? value)
   {
      _absoluteValue = value;
      _subsetSelection = null;
      _minMaxSelection = null;
   }

   private void SetSubsetSelection(IEnumerable<int>? value)
   {
      _absoluteValue = null;
      _subsetSelection = value;
      _minMaxSelection = null;
   }

   private void SetMinMaxSelection(MinMax<int>? value)
   {
      _absoluteValue = null;
      _subsetSelection = null;
      _minMaxSelection = value;
   }
}
