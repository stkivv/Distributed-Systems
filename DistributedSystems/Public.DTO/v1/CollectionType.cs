namespace Public.DTO.v1;

/// <summary>
/// Collection type. Has a list of collections that have said type.
/// </summary>
public class CollectionType
{
    public Guid Id { get; set; }
    
    public string CollectionTypeName { get; set; } = default!;
    
    public ICollection<PlantCollection>? PlantCollections { get; set; }
}