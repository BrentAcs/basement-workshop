public class ConsoleRenderer
{
   public ConsoleColor Foreground
   {
      get => Console.ForegroundColor;
      set => Console.ForegroundColor = value;
   }
   public ConsoleColor Background
   {
      get => Console.BackgroundColor;
      set => Console.BackgroundColor = value;
   }
   public Position CursorPosition
   {
      get
      {
         (int x, int y) = Console.GetCursorPosition(); 
         return new Position { X = x, Y = y};
      }
      set => Console.SetCursorPosition(value.X, value.Y);
   }
}
