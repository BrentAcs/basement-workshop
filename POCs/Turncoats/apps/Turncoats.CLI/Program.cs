// See https://aka.ms/new-console-template for more information

using Spectre.Console;

var cr = new ConsoleRenderer();
// cr.Foreground = ConsoleColor.Magenta;
// cr.CursorPosition = (5, 5); 

cr.DrawSpace(5,10);
cr.DrawSpace(15, 7);
cr.DrawSpace(15, 13);



Console.ReadKey();


