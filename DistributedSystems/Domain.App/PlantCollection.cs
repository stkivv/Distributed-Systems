using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain;

public class PlantCollection : DomainEntityId
{
    [MinLength(1)][MaxLength(128)]
    public string CollectionName { get; set; } = default!;
    
    public ICollection<PlantInCollection>? PlantsInCollections { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}