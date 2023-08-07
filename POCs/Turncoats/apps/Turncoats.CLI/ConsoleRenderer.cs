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

   public (int x, int y) CursorPosition
   {
      get => Console.GetCursorPosition();
      set => Console.SetCursorPosition(value.x, value.y);
   }

   public int CursorXPosition
   {
      get
      {
         (int x, int _) = CursorPosition;
         return x;
      }
      set
      {
         (int _, int y) = CursorPosition;
         CursorPosition = (value, y);
      }
   }

   public int CursorYPosition
   {
      get
      {
         (int _, int y) = CursorPosition;
         return y;
      }
      set
      {
         (int x, int _) = CursorPosition;
         CursorPosition = (x, value);
      }
   }

   public void Write(string? value) => Console.Write(value);

   public void Write(string? value, int times)
   {
      for (int i = 0; i < times; i++)
         Write(value);
   }


   public void DrawSpace(int x, int y, int width = 10, int height = 7)
   {
      Foreground = ConsoleColor.White;
      CursorPosition = (x, y);
      Write("+");
      Write("-", width - 2);
      Write("+");
      for (int i = 0; i < height - 2; i++)
      {
         CursorPosition = (x, y + i + 1);
         Write("|");
         CursorXPosition = x + width - 1;
         Write("|");
      }

      CursorPosition = (x, y + height - 1);
      Write("+");
      Write("-", width - 2);
      Write("+");
   }
}
