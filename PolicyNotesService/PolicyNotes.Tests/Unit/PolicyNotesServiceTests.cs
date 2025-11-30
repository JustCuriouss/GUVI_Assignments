using Microsoft.EntityFrameworkCore;
using PolicyNotes.Data;
using PolicyNotes.Repositories;
using PolicyNotes.Services;
using Xunit;

namespace PolicyNotes.Tests.Unit;

public class PolicyNotesServiceTests
{
    private static AppDbContext CreateDbContext(string name)
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: name)
            .Options;
        return new AppDbContext(opts);
    }

    [Fact]
    public async Task AddNote_AddsAndReturnsNote()
    {
        var db = CreateDbContext(nameof(AddNote_AddsAndReturnsNote));
        var repo = new PolicyNoteRepository(db);
        var service = new PolicyNotesService(repo);

        var created = await service.AddNoteAsync("PN-123", "Internal note A");

        Assert.NotNull(created);
        Assert.Equal("PN-123", created.PolicyNumber);
        Assert.Equal("Internal note A", created.Note);
        Assert.True(created.Id > 0);
    }

    [Fact]
    public async Task GetAll_ReturnsAddedNotes()
    {
        var db = CreateDbContext(nameof(GetAll_ReturnsAddedNotes));
        var repo = new PolicyNoteRepository(db);
        var service = new PolicyNotesService(repo);

        await service.AddNoteAsync("PN-1", "Note 1");
        await service.AddNoteAsync("PN-2", "Note 2");

        var all = (await service.GetAllNotesAsync()).ToList();

        Assert.Equal(2, all.Count);
        Assert.Contains(all, x => x.PolicyNumber == "PN-1");
        Assert.Contains(all, x => x.PolicyNumber == "PN-2");
    }
}
