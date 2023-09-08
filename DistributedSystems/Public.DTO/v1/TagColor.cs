namespace Public.DTO.v1;
/// <summary>
/// Color information for a tag. Has a collection of tags that use this color.
/// </summary>
public class TagColor
{
    public Guid Id { get; set; }

    public string ColorName { get; set; } = default!;
    
    public string ColorHex { get; set; } = default!;
    
    //public ICollection<Tag>? Tags { get; set; }
}