using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.DTO;

public class CollectionType : DomainEntityId
{
    [MinLength(1)][MaxLength(128)]
    public string CollectionTypeName { get; set; } = default!;
    
    public ICollection<PlantCollection>? PlantCollections { get; set; }
}