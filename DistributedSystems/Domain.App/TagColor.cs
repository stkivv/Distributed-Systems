using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

public class TagColor : DomainEntityId
{

    [MinLength(1)][MaxLength(128)]
    public string ColorName { get; set; } = default!;
    
    [MinLength(1)][MaxLength(128)]
    public string ColorHex { get; set; } = default!;
    
    public ICollection<Tag>? Tags { get; set; }

}
