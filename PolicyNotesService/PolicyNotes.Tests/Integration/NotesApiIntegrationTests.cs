using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PolicyNotes.Data;
using PolicyNotes.DTOs;
using PolicyNotes.Models;
using Xunit;

namespace PolicyNotes.Tests.Integration;

public class NotesApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public NotesApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Remove previous registrations of DbContext
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                // Add shared in-memory database (MUST have same name)
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });
            });
        });
    }

    [Fact]
    public async Task PostNotes_Returns_Created()
    {
        var client = _factory.CreateClient();

        var dto = new PolicyNoteCreateDto { PolicyNumber = "PN-INT-1", Note = "Integration note" };
        var resp = await client.PostAsJsonAsync("/notes", dto);

        Assert.Equal(HttpStatusCode.Created, resp.StatusCode);
        // Verify location header set
        Assert.True(resp.Headers.Location != null);
        var created = await resp.Content.ReadFromJsonAsync<PolicyNote>();
        Assert.NotNull(created);
        Assert.Equal(dto.PolicyNumber, created!.PolicyNumber);
    }

    [Fact]
    public async Task GetNotes_Returns_Ok()
    {
        var client = _factory.CreateClient();

        // seed via POST
        await client.PostAsJsonAsync("/notes", new PolicyNoteCreateDto { PolicyNumber = "PN-2", Note = "n2" });

        var resp = await client.GetAsync("/notes");
        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
        var list = await resp.Content.ReadFromJsonAsync<List<PolicyNote>>();
        Assert.NotNull(list);
        Assert.True(list!.Count >= 1);
    }

    [Fact]
    public async Task GetNoteById_Returns_200_WhenFound()
    {
        var client = _factory.CreateClient();

        var post = await client.PostAsJsonAsync("/notes", new PolicyNoteCreateDto { PolicyNumber = "PN-3", Note = "n3" });
        var created = await post.Content.ReadFromJsonAsync<PolicyNote>();
        Assert.NotNull(created);

        var resp = await client.GetAsync($"/notes/{created!.Id}");
        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
        var fetched = await resp.Content.ReadFromJsonAsync<PolicyNote>();
        Assert.Equal(created.Id, fetched!.Id);
    }

    [Fact]
    public async Task GetNoteById_Returns_404_WhenMissing()
    {
        var client = _factory.CreateClient();

        var resp = await client.GetAsync($"/notes/{int.MaxValue}");
        Assert.Equal(HttpStatusCode.NotFound, resp.StatusCode);
    }
}
