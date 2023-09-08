using Domain.Base;

namespace BLL.DTO;

public class PlantTag : DomainEntityId
{
    public Guid TagId { get; set; }
    public Tag? Tag { get; set; }
    
    public Guid PlantId { get; set; }
    public Plant? Plant { get; set; }
}