namespace Public.DTO.v1;
/// <summary>
/// Plant collection. 
/// </summary>
public class PlantCollection
{
    public Guid Id { get; set; }

    public string CollectionName { get; set; } = default!;
    
    public ICollection<PlantInCollection>? PlantsInCollections { get; set; }

    public Guid AppUserId { get; set; }
}