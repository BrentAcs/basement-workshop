using System.Drawing;
using System.Net;
using Blazor.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Turncoats.WebUI.Services;

namespace Turncoats.WebUI.Pages;

public partial class TurncoatsMap
{
   public ElementReference divCanvas;
   public BECanvasComponent myCanvas;

   [Inject]
   public IJSRuntime jsRuntime { get; set; }

   [Inject]
   public IMapCanvasRuntime MapCanvasRuntime { get; set; }

   protected override void OnInitialized()
   {
      base.OnInitialized();
   }

   public async void OnClick(MouseEventArgs e)
   {
   }

   public async void OnMouseOver(MouseEventArgs e)
   {
      Console.WriteLine("OnMouseOver.");
   }

   public async void OnMouseEnter(MouseEventArgs e)
   {
      Console.WriteLine("OnMouseEnter....");
      var info = await MapCanvasRuntime.GetClientInfo(divCanvas);
      var mousePos = new PointF
      {
         X = (float)(e.ClientX - info.Offset.X),
         Y = (float)(e.ClientY - info.Offset.Y)
      };
      Console.WriteLine($"   mouse Pos: {mousePos.X}, {mousePos.Y}");
   }
}
