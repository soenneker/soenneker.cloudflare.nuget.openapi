using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.NuGet.OpenApi.Tests;

public sealed class CloudflareNuGetOpenApiTests
{
    [Test]
    public async Task Contract_exposes_feed_and_mutation_operations()
    {
        using var document = JsonDocument.Parse(await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "openapi.json")));
        var paths = document.RootElement.GetProperty("paths");
        await Assert.That(paths.GetProperty("/v3/index.json").TryGetProperty("get", out _)).IsTrue();
        await Assert.That(paths.GetProperty("/api/v2/package").TryGetProperty("put", out _)).IsTrue();
        await Assert.That(paths.GetProperty("/api/v2/package/{id}/{version}").TryGetProperty("delete", out _)).IsTrue();
        await Assert.That(paths.GetProperty("/api/v2/package/{id}/{version}").TryGetProperty("post", out _)).IsTrue();
    }
}
