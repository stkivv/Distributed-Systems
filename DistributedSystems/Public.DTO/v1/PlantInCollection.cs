namespace Public.DTO.v1;
/// <summary>
/// Plant in collection. An in-between element that represents a connection between a collection and a plant.
/// </summary>
public class PlantInCollection
{
    public Guid Id { get; set; }

    public Guid PlantCollectionId { get; set; }
    
    public Guid PlantId { get; set; }
}