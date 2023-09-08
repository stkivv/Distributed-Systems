using Domain.Base;

namespace Domain;

public class PlantInCollection : DomainEntityId
{

    public Guid PlantCollectionId { get; set; }
    public PlantCollection? PlantCollection { get; set; }
    
    public Guid PlantId { get; set; }
    public Plant? Plant { get; set; }
}