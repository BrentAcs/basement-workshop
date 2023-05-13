using Bass.Shared.Extensions;

namespace Bass.Shared.Utilities;

public class DoubleValueRequest
{
   private double? _absoluteValue;
   private IEnumerable<double>? _subsetSelection;
   private MinMax<double>? _minMaxSelection;

   public DoubleValueRequest()
   {
   }

   public DoubleValueRequest(double absoluteValue)
   {
      _absoluteValue = absoluteValue;
   }

   public DoubleValueRequest(IEnumerable<double>? subsetSelection)
   {
      _subsetSelection = subsetSelection;
   }

   public DoubleValueRequest(double min, double max) : this(new MinMax<double>(min, max))
   {
   }

   public DoubleValueRequest(MinMax<double>? minMaxSelection)
   {
      _minMaxSelection = minMaxSelection;
   }

   public double? AbsoluteValue
   {
      get => _absoluteValue;
      set => SetAbsoluteValue(value);
   }

   public IEnumerable<double>? SubsetSelection
   {
      get => _subsetSelection;
      set => SetSubsetSelection(value);
   }

   public MinMax<double>? MinMaxSelection
   {
      get => _minMaxSelection;
      set => SetMinMaxSelection(value);
   }

   public double GetValue(IRng rng)
   {
      if (_absoluteValue is not null)
         return _absoluteValue.Value;

      if (_subsetSelection is not null)
         return rng.Next(_subsetSelection);
      
      if (_minMaxSelection is not null)
         return rng.Next(_minMaxSelection);

      throw new NotImplementedException();
   }
   
   private void SetAbsoluteValue(double? value)
   {
      _absoluteValue = value;
      _subsetSelection = null;
      _minMaxSelection = null;
   }

   private void SetSubsetSelection(IEnumerable<double>? value)
   {
      _absoluteValue = null;
      _subsetSelection = value;
      _minMaxSelection = null;
   }

   private void SetMinMaxSelection(MinMax<double>? value)
   {
      _absoluteValue = null;
      _subsetSelection = null;
      _minMaxSelection = value;
   }
}
