namespace Bass.Shared.Utilities;

public class EnumValuesRequest<T> : ValuesRequest<T> where T : Enum
{
   public EnumValuesRequest() : this(1)
   {
   }

   public EnumValuesRequest(int amountRequested)
   {
      SubsetSelection = (T [])Enum.GetValues(typeof(T));
      AmountRequested = amountRequested;
   }
}
