namespace Public.DTO.v1;
/// <summary>
/// Photo. Tied to a specific plant and is used to represent said plant.
/// </summary>
public class Photo
{
    public Guid Id { get; set; }

    public string ImageUrl { get; set; } = default!;
    
    public string? ImageDescription { get; set; }
    
    public Guid PlantId { get; set; }
}