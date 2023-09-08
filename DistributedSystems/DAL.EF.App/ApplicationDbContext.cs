using Domain;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DAL;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<CollectionType> CollectionTypes { get; set; } = default!;
    public DbSet<HistoryEntry> HistoryEntries { get; set; } = default!;
    public DbSet<HistoryEntryType> HistoryEntryTypes { get; set; } = default!;
    public DbSet<Month> Months { get; set; } = default!;
    public DbSet<Pest> Pests { get; set; } = default!;
    public DbSet<PestSeverity> PestSeverities { get; set; } = default!;
    public DbSet<PestType> PestTypes { get; set; } = default!;
    public DbSet<Photo> Photos { get; set; } = default!;
    public DbSet<Plant> Plants { get; set; } = default!;
    public DbSet<PlantCollection> PlantCollections { get; set; } = default!;
    public DbSet<PlantInCollection> PlantsInCollections { get; set; } = default!;
    public DbSet<PlantTag> PlantTags { get; set; } = default!;
    public DbSet<Reminder> Reminders { get; set; } = default!;
    public DbSet<ReminderActiveMonth> ReminderActiveMonths { get; set; } = default!;
    public DbSet<ReminderType> ReminderTypes { get; set; } = default!;
    public DbSet<SizeCategory> SizeCategories { get; set; } = default!;
    public DbSet<Tag> Tags { get; set; } = default!;
    public DbSet<TagColor> TagColors { get; set; } = default!;
    public DbSet<EventType> EventTypes { get; set; } = default!;

    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = default!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        modelBuilder.Entity<Plant>()
            .HasMany(p => p.PlantTags)
            .WithOne(t => t.Plant)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Plant>()
            .HasMany(p => p.PlantInCollections)
            .WithOne(t => t.Plant)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Plant>()
            .HasMany(p => p.Photos)
            .WithOne(t => t.Plant)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Plant>()
            .HasMany(p => p.HistoryEntries)
            .WithOne(t => t.Plant)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Plant>()
            .HasMany(p => p.Pests)
            .WithOne(t => t.Plant)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Plant>()
            .HasMany(p => p.Reminders)
            .WithOne(t => t.Plant)
            .OnDelete(DeleteBehavior.Cascade);
        
        
        modelBuilder.Entity<Tag>()
            .HasMany(t => t.PlantTags)
            .WithOne(t => t.Tag)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<PlantCollection>()
            .HasMany(c => c.PlantsInCollections)
            .WithOne(t => t.PlantCollection)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Reminder>()
            .HasMany(r => r.ReminderActiveMonths)
            .WithOne(m => m.Reminder)
            .OnDelete(DeleteBehavior.Cascade);
    }
}