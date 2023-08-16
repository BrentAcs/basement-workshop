using System.Drawing;

namespace Turncoats.WebUI.Services;

public class CanvasClientInfo
{
   public Size ClientSize { get; set; } = new();
   public Point Offset { get; set; } = new();
}
