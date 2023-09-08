using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain;

public class Plant : DomainEntityId
{
    [MinLength(1)][MaxLength(128)]
    public string PlantName { get; set; } = default!;
    
    [MaxLength(256)]
    public string? Description { get; set; }
    
    [MaxLength(128)]
    public string? PlantFamily { get; set; }
    
    [MaxLength(128)]
    public string? ScientificName { get; set; }
    
    public ICollection<Photo>? Photos { get; set; }
    public ICollection<HistoryEntry>? HistoryEntries { get; set; }
    public ICollection<PlantTag>? PlantTags { get; set; }
    public ICollection<Pest>? Pests { get; set; }
    public ICollection<Reminder>? Reminders { get; set; }
    public ICollection<PlantInCollection>? PlantInCollections { get; set; }


    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public Guid SizeCategoryId { get; set; }
    public SizeCategory? SizeCategory { get; set; }
}