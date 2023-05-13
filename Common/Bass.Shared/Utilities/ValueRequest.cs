using Bass.Shared.Extensions;

namespace Bass.Shared.Utilities;

public class ValueRequest<T>
{
   private bool _useAbsoluteValue;
   private T? _absoluteValue;
   private IEnumerable<T>? _subsetSelection;

   public ValueRequest()
   {
   }

   public ValueRequest(T absoluteValue)
   {
      _absoluteValue = absoluteValue;
      _useAbsoluteValue = true;
   }

   public ValueRequest(IEnumerable<T>? subsetSelection)
   {
      _subsetSelection = subsetSelection;
   }
   
   public T? AbsoluteValue
   {
      get => _absoluteValue;
      set => SetAbsoluteValue(value);
   }

   public bool UseAbsoluteValue => _useAbsoluteValue;

   public IEnumerable<T>? SubsetSelection
   {
      get => _subsetSelection;
      set => SetSubsetSelection(value);
   }

   public T GetValue(IRng rng)
   {
      if (_useAbsoluteValue)
         return _absoluteValue!;

      if (_subsetSelection is not null)
         return rng.Next(_subsetSelection);
      
      throw new NotImplementedException();
   }
   
   private void SetAbsoluteValue(T? value)
   {
      if (value is null)
         throw new ArgumentNullException(nameof(value));
      
      _absoluteValue = value;
      _useAbsoluteValue = true;
      _subsetSelection = null;
   }

   private void SetSubsetSelection(IEnumerable<T>? value)
   {
      _absoluteValue = default;
      _useAbsoluteValue = false;
      _subsetSelection = value;
   }
}
