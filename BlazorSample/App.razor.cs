using Avalonia;
using Avalonia.Web.Blazor;

namespace MyApp.Web;

public partial class App
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        WebAppBuilder.Configure<FluentAvaloniaSamples.App>()
            .With(new SkiaOptions { CustomGpuFactory = null,  }) // disable GL/GPU, fall back to raster
            .SetupWithSingleViewLifetime()
            ;
    }
}
