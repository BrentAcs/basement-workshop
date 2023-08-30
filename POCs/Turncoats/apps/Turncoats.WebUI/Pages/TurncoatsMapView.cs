using Excubo.Blazor.Canvas;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Turncoats.Game;
using Turncoats.WebUI.Services;

namespace Turncoats.WebUI.Pages;

public partial class TurncoatsMapView
{
   private IMapRenderer _mapRenderer = new MapRenderer();
   private TheGame _game = new();

   //private ElementReference _helperCanvas;
   private Canvas _helperCanvas;
   // public BECanvasComponent _beCanvas;

   [Inject]
   public IJSRuntime jsRuntime { get; set; }
   // [Inject]
   // public IMapCanvasRuntime MapCanvasRuntime { get; set; }
   
   protected override async Task OnAfterRenderAsync(bool firstRender)
   {
      if (firstRender)
      {
         // var info = await MapCanvasRuntime.GetClientInfo(_helperCanvas);

         
         
         await using var ctx = await _helperCanvas.GetContext2DAsync();
         
         await ctx.FontAsync("48px solid");
         await ctx.FillTextAsync("Hello", 0, 150);

         await ctx.FillStyleAsync("blue");
         await ctx.BeginPathAsync();
         await ctx.EllipseAsync(100,100,50,50,0,0,360);
         await ctx.StrokeAsync();

         // await ctx.FillRectAsync(20, 20, 100, 100);

         // var info = await MapCanvasRuntime.GetClientInfo(_divCanvas);
         // var options = new MapRendererOptions
         // {
         //    ClientSize = info.ClientSize,
         //    Padding = new Size(info.Offset.X, info.Offset.Y)
         // };
         //
         // _canvas2DContext = await _beCanvas.CreateCanvas2DAsync().ConfigureAwait(false);
         // //await _canvas2DContext.SetTextBaselineAsync(TextBaseline.Top);
         // await _mapRenderer.Render(_canvas2DContext, options, _game.Map).ConfigureAwait(false);
      }
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
      // var info = await MapCanvasRuntime.GetClientInfo(divCanvas);
      // var mousePos = new PointF
      // {
      //    X = (float)(e.ClientX - info.Offset.X),
      //    Y = (float)(e.ClientY - info.Offset.Y)
      // };
      // Console.WriteLine($"   mouse Pos: {mousePos.X}, {mousePos.Y}, {info.ClientSize.Width}, {info.ClientSize.Height}");
      
      
      // var ctx = await _beCanvas.CreateCanvas2DAsync();
      // await ctx.SetFillStyleAsync("Gray");
      // await ctx.FillRectAsync(0, 0, 1200,800);
   }
}

