namespace Public.DTO.v1;
/// <summary>
/// pest type. has a collection of pests that use it.
/// </summary>
public class PestType
{
    public Guid Id { get; set; }

    public string PestTypeName { get; set; } = default!;
    
}