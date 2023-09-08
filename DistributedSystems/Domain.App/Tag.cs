using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain;

public class Tag : DomainEntityId
{

    [MinLength(1)][MaxLength(128)]
    public string TagLabel { get; set; } = default!;
    
    public ICollection<PlantTag>? PlantTags { get; set; }

    public Guid TagColorId { get; set; }
    public TagColor? TagColor { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

}