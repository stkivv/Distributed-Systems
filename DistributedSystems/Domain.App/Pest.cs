using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

public class Pest : DomainEntityId
{
    [MaxLength(256)]
    public string? PestComment { get; set; }
    
    public DateTime PestDiscoveryTime { get; set; }

    public Guid PlantId { get; set; }
    public Plant? Plant { get; set; }
    
    public Guid PestTypeId { get; set; }
    public PestType? PestType { get; set; }
    
    public Guid PestSeverityId { get; set; }
    public PestSeverity? PestSeverity { get; set; }
}