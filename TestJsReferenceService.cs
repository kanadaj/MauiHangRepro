using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiHangRepro;
public class TestJsReferenceService : IAsyncDisposable
{
    private readonly IJSRuntime jsRuntime;
    private IJSObjectReference? module = null;
    private const string SomeJsModule = "/site.js";
    public TestJsReferenceService(IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime;
    }

    public async Task LoadModuleAsync(CancellationToken cancellationToken = default)
    {
        module = await jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", cancellationToken, SomeJsModule);
    }

    public async ValueTask DisposeAsync()
    {
        if (module is not null)
        {
            // The app hangs here
            await module.DisposeAsync();
        }
    }
}