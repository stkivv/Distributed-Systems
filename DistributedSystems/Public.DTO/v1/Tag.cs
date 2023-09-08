namespace Public.DTO.v1;
/// <summary>
/// Tag for a plant. Tied to users account and a specific color.
/// </summary>
public class Tag
{
    public Guid Id { get; set; }
    
    public string TagLabel { get; set; } = default!;
    
    //public ICollection<PlantTag>? PlantTags { get; set; }

    public TagColor? TagColor { get; set; }
    
    public Guid AppUserId { get; set; }
}