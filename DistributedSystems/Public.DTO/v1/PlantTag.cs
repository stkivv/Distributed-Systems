namespace Public.DTO.v1;
/// <summary>
/// Plant tag. A tag that is tied to a specific plant.
/// </summary>
public class PlantTag
{
    public Guid Id { get; set; }

    public Guid TagId { get; set; }
    
    public Guid PlantId { get; set; }
}