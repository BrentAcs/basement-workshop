namespace ProjectKitt.WinForms.Services.Rendering;


public interface IRendererFactory
{
   
}

public class RendererFactory
{
   
}

public interface IRenderer
{
   // ScaleFactor ScaleFactor { get; set; }
}

public interface IStaticObjectRenderer : IRenderer
{
   
}


public abstract class Renderer : IRenderer
{
   // public ScaleFactor ScaleFactor { get; set; }
}