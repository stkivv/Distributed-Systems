using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.ApiControllers;
using Xunit.Abstractions;

namespace Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly PlantRepository _repo;
    private readonly ApplicationDbContext _ctx;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        // set up mock database - inMemory
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new ApplicationDbContext(optionsBuilder.Options);

        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        using var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = logFactory.CreateLogger<PlantController>();

        // SUT
        _repo = new PlantRepository(_ctx);

    }
    
    
    [Fact]
    public async Task TestGetPlants()
    {
        await SeedDataAsync();

        var result = await _repo.AllAsync();

        var resultList = result.ToList();
        Assert.Equal(1, resultList.Count());
        Assert.Equal("TestPlant", resultList.First().PlantName);

    }
    
    [Fact]
    public async Task TestFindPlant()
    {
        await SeedDataAsync();

        var result = await _repo.FindAsync(Guid.Parse("588e88a2-5936-4009-9793-8c95819ea463"));
        
        Assert.Equal("TestPlant", result!.PlantName);

    }
    
    [Fact]
    public async Task TestRemovePlant()
    {
        await SeedDataAsync();
        await _repo.RemoveAsync(Guid.Parse("588e88a2-5936-4009-9793-8c95819ea463"));
        await _ctx.SaveChangesAsync();
        var result = await _repo.AllAsync();
        var resultList = result.ToList();
        Assert.Equal(0, resultList.Count());

    }
    
    [Fact]
    public async Task TestAddPlant()
    {
        await SeedDataAsync();
        var size = await _ctx.SizeCategories.AddAsync(new Domain.SizeCategory()
        {
            Id = Guid.Parse("ec42277a-2439-4641-ad58-ea85eccf428d"),
            SizeName = "testSize2"
        });
        
        _ctx.Plants.Add(new Domain.Plant()
        {
            Id = Guid.Parse("9ff42673-ac60-454e-87b3-1285c1cf79c1"),
            PlantName = "TestPlant2",
            SizeCategoryId = size.Entity.Id
        });
        await _ctx.SaveChangesAsync();
        
        var result = await _repo.AllAsync();
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count());
        Assert.Equal("TestPlant2", resultList.Last().PlantName);

    }
    
    private async Task SeedDataAsync()
    {
        var size = await _ctx.SizeCategories.AddAsync(new Domain.SizeCategory()
        {
            Id = Guid.Parse("74975565-5b20-4173-af73-e616594a6720"),
            SizeName = "testSize"
        });
        
        _ctx.Plants.Add(new Domain.Plant()
        {
            Id = Guid.Parse("588e88a2-5936-4009-9793-8c95819ea463"),
            PlantName = "TestPlant",
            SizeCategoryId = size.Entity.Id
        });
        await _ctx.SaveChangesAsync();

    }

}