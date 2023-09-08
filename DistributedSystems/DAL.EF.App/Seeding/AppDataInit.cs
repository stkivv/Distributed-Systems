using Domain;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seeding;

public static class AppDataInit
{
    private static readonly Guid ClientId = Guid.Parse("588e88a2-5936-4009-9793-8c95819ea463");
    private static readonly Guid AdminId = Guid.Parse("74975565-5b20-4173-af73-e616594a6720");
    
    public static void MigrateDatabase(ApplicationDbContext ctx)
    {
        ctx.Database.Migrate();
    }
    
    public static void DropDatabase(ApplicationDbContext ctx)
    {
        ctx.Database.EnsureDeleted();
    }
    
    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        var adminRole = new AppRole()
        {
            Id = Guid.Parse("ec42277a-2439-4641-ad58-ea85eccf428d"),
            Name = "Admin"
        };
        var clientRole = new AppRole()
        {
            Id = Guid.Parse("9ff42673-ac60-454e-87b3-1285c1cf79c1"),
            Name = "Client"
        };
        var createdAdminRole = roleManager.CreateAsync(adminRole).Result;
        var createdClientRole = roleManager.CreateAsync(clientRole).Result;

        (Guid Id, string email, string pwd) clientUserData = (ClientId, "foobar@app.com", "Foo.bar.1");
        (Guid Id, string email, string pwd) adminUserData = (AdminId, "admin@app.com", "Master.user.1");
        
        var clientUser = userManager.FindByEmailAsync(clientUserData.email).Result;
        if (clientUser == null)
        {
            clientUser = new AppUser()
            {
                Id = clientUserData.Id,
                Email = clientUserData.email,
                UserName = clientUserData.email,
                FirstName = "Example",
                LastName = "Client",
                EmailConfirmed = true
            };

            var result = userManager.CreateAsync(clientUser, clientUserData.pwd).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException("Cannot seed client user");
            }
        }
        
        var adminUser = userManager.FindByEmailAsync(adminUserData.email).Result;
        if (adminUser == null)
        {
            adminUser = new AppUser()
            {
                Id = adminUserData.Id,
                Email = adminUserData.email,
                UserName = adminUserData.email,
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true
            };

            var result = userManager.CreateAsync(adminUser, adminUserData.pwd).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException("Cannot seed admin user");
            }
        }
        
        var clientAddToRoleResult = userManager.AddToRoleAsync(clientUser, "Client").Result;
        var adminAddToRoleResult = userManager.AddToRoleAsync(adminUser, "Admin").Result;
    }
    
    public static void SeedAppData(ApplicationDbContext ctx)
    {
        var eventType = ctx.Add(new EventType()
        {
            Id = Guid.Parse("a93e8ea1-0ce2-4305-ac78-03f692630c35"),
            EventTypeName = "Watering"
        });

        var hisEntryType = ctx.Add(new HistoryEntryType()
        {
            Id = Guid.Parse("ea389f90-0847-4712-a0fb-8b96e79c9c35"),
            EntryTypeName = "TestHistoryEntryType",
            EventTypeId = eventType.Entity.Id
        });
        
        var sizeCategory = ctx.Add(new SizeCategory()
        {
            Id = Guid.Parse("84ac3351-14fd-47fb-b33e-81fe7b72157e"),
            SizeName = "Large"
        });
        
        var sizeCategory2 = ctx.Add(new SizeCategory()
        {
            Id = Guid.Parse("961e11a6-3920-430d-ad61-7558dcddad9a"),
            SizeName = "Medium"
        });
        
        var sizeCategory3 = ctx.Add(new SizeCategory()
        {
            Id = Guid.Parse("ade8b5d6-25e8-4b43-9f2c-8883d735074e"),
            SizeName = "Small"
        });
        
        var plant = ctx.Add(new Plant()
        {
            Id = Guid.Parse("71874c36-2b22-497f-ac1a-2520332c65df"),
            SizeCategoryId = sizeCategory.Entity.Id,
            AppUserId = ClientId,
            PlantName = "Aloe vera",
            PlantFamily = "Asphodelaceae",
            ScientificName = "Aloe barbadensis miller",
            Description = "Aloe vera is a succulent plant species of the genus Aloe, which originates from the Arabian Peninsula," +
                          " but grows wild in tropical, semi-tropical, and arid climates around the world."
        });
        
        ctx.Add(new HistoryEntry()
        {
            Id = Guid.Parse("54c215c1-4fe0-4a92-8b47-9c6131a63413"),
            EntryComment = "Test history entry",
            EntryTime = DateTime.UtcNow.AddDays(-2),
            HistoryEntryTypeId = hisEntryType.Entity.Id,
            PlantId = plant.Entity.Id
        });
        
        ctx.Add(new Month()
        {
            Id = Guid.Parse("8c1e5e13-ec55-4a48-8305-00648ca9e5e3"),
            MonthName = "January",
            MonthNr = 1
        });
        
        ctx.Add(new Month()
        {
            Id = Guid.Parse("b8d5f988-c5b0-4215-bb48-6cd4776b3a56"),
            MonthName = "February",
            MonthNr = 2
        });
        
        ctx.Add(new Month()
        {
            Id = Guid.Parse("51dcb332-47e8-44af-8194-556c055ff4ee"),
            MonthName = "March",
            MonthNr = 3
        });
        
        ctx.Add(new Month()
        {
            Id = Guid.Parse("46a51502-102b-4fb0-9796-a12c69621964"),
            MonthName = "April",
            MonthNr = 4
        });
        
        var month = ctx.Add(new Month()
        {
            Id = Guid.Parse("9db0d0d8-e283-4273-a176-d8d6e241ec47"),
            MonthName = "May",
            MonthNr = 5
        });
        
        ctx.Add(new Month()
        {
            Id = Guid.Parse("52b0dbc6-4958-4690-b8b4-559a7649bfcf"),
            MonthName = "June",
            MonthNr = 6
        });
        
        ctx.Add(new Month()
        {
            Id = Guid.Parse("6dc96e05-dd43-4a86-9bd6-a42eae3e1e02"),
            MonthName = "July",
            MonthNr = 7
        });
        
        ctx.Add(new Month()
        {
            Id = Guid.Parse("989a4465-7c0b-4d2d-a9e0-c9a81fa820cb"),
            MonthName = "August",
            MonthNr = 8
        });
        
        ctx.Add(new Month()
        {
            Id = Guid.Parse("87c1f5a9-8cd4-4321-ab62-b99124227c2c"),
            MonthName = "September",
            MonthNr = 9
        });
        
        ctx.Add(new Month()
        {
            Id = Guid.Parse("99b09323-8a61-40b6-bcee-1b537b709e0f"),
            MonthName = "October",
            MonthNr = 10
        });
        
        ctx.Add(new Month()
        {
            Id = Guid.Parse("2b649b32-c581-4a4a-888b-7670737d7fc9"),
            MonthName = "November",
            MonthNr = 11
        });
        
        ctx.Add(new Month()
        {
            Id = Guid.Parse("b9fa17c3-e1c5-4a78-94fa-fbc7fa9b9633"),
            MonthName = "December",
            MonthNr = 12
        });
        
        var pestSeverity = ctx.Add(new PestSeverity()
        {
            Id = Guid.Parse("baf7d0a2-22c4-4a3e-a921-b2288ced417e"),
            PestSeverityName = "Severe"
        });
        
        ctx.Add(new PestSeverity()
        {
            Id = Guid.Parse("e597d22c-1be4-4ec6-86e7-200616a5c444"),
            PestSeverityName = "Mild"
        });
        
        var pestType = ctx.Add(new PestType()
        {
            Id = Guid.Parse("fd0ab6ae-b6ba-4756-9988-6a516b3a9302"),
            PestTypeName = "mites"
        });
        
        ctx.Add(new PestType()
        {
            Id = Guid.Parse("1d8cf19a-cb3f-4949-b817-5f1e51a5d422"),
            PestTypeName = "fungus"
        });
        
        ctx.Add(new Pest()
        {
            Id = Guid.Parse("62de723b-dd6f-4a97-8168-9189a6899b6c"),
            PestComment = "This pest is crazy fr",
            PestDiscoveryTime = DateTime.UtcNow,
            PlantId = plant.Entity.Id,
            PestSeverityId = pestSeverity.Entity.Id,
            PestTypeId = pestType.Entity.Id
        });
        
        ctx.Add(new Photo()
        {
            Id = Guid.Parse("59ff5760-5098-45bc-b619-07934722ef14"),
            ImageUrl = "https://hips.hearstapps.com/hmg-prod/images/aloe-vera-plant-outside-jpg-1522875135.jpg",
            ImageDescription = "aloe vera",
            PlantId = plant.Entity.Id
        });
        
        var plantCollection = ctx.Add(new PlantCollection()
        {
            Id = Guid.Parse("fa6ed4d6-af7b-4ee3-a956-bf61cc6a281d"),
            CollectionName = "test collection",
            AppUserId = ClientId,
        });
        
        ctx.Add(new PlantInCollection()
        {
            Id = Guid.Parse("8285f629-ff15-46a3-8253-af1222176e5f"),
            PlantId = plant.Entity.Id,
            PlantCollectionId = plantCollection.Entity.Id
        });
        
        var tagCol = ctx.Add(new TagColor()
        {
            Id = Guid.Parse("fc1df8a5-c4fb-4243-9a56-35fd0ed50f30"),
            ColorName = "green",
            ColorHex = "#1a911a"
        });
        
        ctx.Add(new TagColor()
        {
            Id = Guid.Parse("42784e66-3ac3-47a6-aeb2-3e2e7f90bfa9"),
            ColorName = "red",
            ColorHex = "#990F02"
        });
        
        var tag = ctx.Add(new Tag()
        {
            Id = Guid.Parse("c2690588-1691-403d-a15b-adc6c207a774"),
            TagLabel = "test tag",
            AppUserId = ClientId,
            TagColorId = tagCol.Entity.Id
        });
        
        ctx.Add(new PlantTag()
        {
            Id = Guid.Parse("d8767448-d75c-42d6-8075-9e04eca6db30"),
            PlantId = plant.Entity.Id,
            TagId = tag.Entity.Id
        });
        
        var remindType = ctx.Add(new ReminderType()
        {
            Id = Guid.Parse("d8541633-45aa-421e-89b4-bc74cf0f9966"),
            ReminderTypeName = "Test reminder type",
            EventTypeId = eventType.Entity.Id
        });
        
        var reminder = ctx.Add(new Reminder()
        {
            Id = Guid.Parse("bc65fafd-7780-4943-8994-90fa538d474d"),
            PlantId = plant.Entity.Id,
            ReminderFrequency = DateTime.UtcNow,
            ReminderMessage = "test reminder",
            ReminderTypeId = remindType.Entity.Id,
            AppUserId = ClientId
        });
        
        ctx.Add(new ReminderActiveMonth()
        {
            Id = Guid.Parse("fed4874b-c55f-4dfc-9bb2-d75ed619f993"),
            MonthId = month.Entity.Id,
            ReminderId = reminder.Entity.Id
        });
        
        ctx.SaveChanges();
    }
}