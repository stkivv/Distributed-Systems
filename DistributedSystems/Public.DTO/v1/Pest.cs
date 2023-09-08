namespace Public.DTO.v1;
/// <summary>
/// Pest. Can have different types and severities.
/// </summary>
public class Pest
{
    public Guid Id { get; set; }

    public string? PestComment { get; set; }
    
    public DateTime PestDiscoveryTime { get; set; }

    public Guid PlantId { get; set; }
    
    public PestType? PestType { get; set; }
    
    public PestSeverity? PestSeverity { get; set; }
}