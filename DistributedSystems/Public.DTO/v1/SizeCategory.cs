namespace Public.DTO.v1;
/// <summary>
/// Size category for a plant.
/// </summary>
public class SizeCategory
{
    public Guid Id { get; set; }

    //public ICollection<Plant>? Plants { get; set; }

    public string SizeName { get; set; } = default!;
}