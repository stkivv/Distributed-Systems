using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

public class PestType : DomainEntityId
{

    [MinLength(1)][MaxLength(128)]
    public string PestTypeName { get; set; } = default!;
    
    public ICollection<Pest>? Pests { get; set; }

}