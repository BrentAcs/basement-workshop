using System.Drawing;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Turncoats.WebUI.Services;

public interface IMapCanvasRuntime
{
   Task<CanvasClientInfo> GetClientInfo(ElementReference elementReference);
}

public class MapCanvasRuntime : IMapCanvasRuntime
{
   private readonly IJSRuntime _jsRuntime;

   public MapCanvasRuntime(IJSRuntime jsRuntime)
   {
      _jsRuntime = jsRuntime;
   }

   public async Task<CanvasClientInfo> GetClientInfo(ElementReference elementReference)
   {
      var data = await _jsRuntime.InvokeAsync<string>("getDivCanvasClientInfo", new object[] {elementReference});
      var clientInfo = (JObject)JsonConvert.DeserializeObject(data)!;

      return new CanvasClientInfo
      {
         ClientSize = new Size
         {
            Width = clientInfo.Value<int>("clientWidth"),
            Height = clientInfo.Value<int>("clientHeight"),
         },
         Offset = new Point
         {
            X = clientInfo.Value<int>("offsetLeft"),
            Y = clientInfo.Value<int>("offsetTop")
         }
      };
   }
}

public class CanvasClientInfo
{
   public Size ClientSize { get; set; } = new();
   public Point Offset { get; set; } = new();
}
