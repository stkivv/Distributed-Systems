namespace Public.DTO.v1;
/// <summary>
/// Pest severity. Shows how serious/how far developed the pest is.
/// </summary>
public class PestSeverity
{
    public Guid Id { get; set; }

    public string PestSeverityName { get; set; } = default!;
    
}