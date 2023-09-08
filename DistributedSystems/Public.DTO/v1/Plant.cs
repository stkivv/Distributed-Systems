namespace Public.DTO.v1;
/// <summary>
/// Plant. has several collections - photos, history entries, tags, pests and reminders. 
/// </summary>
public class Plant
{
    public Guid Id { get; set; }
    
    public string PlantName { get; set; } = default!;
    
    public string? Description { get; set; }
    
    public string? PlantFamily { get; set; }
    
    public string? ScientificName { get; set; }
    
    public ICollection<Photo>? Photos { get; set; }
    public ICollection<HistoryEntry>? HistoryEntries { get; set; }
    //public ICollection<PlantTag>? PlantTags { get; set; }
    public ICollection<Pest>? Pests { get; set; }
    public ICollection<Reminder>? Reminders { get; set; }
    //public ICollection<PlantInCollection>? PlantInCollections { get; set; }


    public Guid AppUserId { get; set; }
    
    public SizeCategory? SizeCategory { get; set; }
    
    public ICollection<PlantCollection>? PlantCollections { get; set; }
    public ICollection<Tag>? Tags { get; set; }
}