namespace Public.DTO.v1;

/// <summary>
/// Includes all the options the user can choose from when for creating a new plant object.
/// On post, collections must include only selected options.
/// </summary>
public class OptionsForCreatePlant
{
    public ICollection<Tag>? Tags { get; set; }
    
    public ICollection<SizeCategory>? SizeCategories { get; set; }

    public ICollection<PlantCollection>? PlantCollections { get; set; }
    
    public Guid? PlantId { get; set; }
}