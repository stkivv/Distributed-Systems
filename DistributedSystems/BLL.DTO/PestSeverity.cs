using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.DTO;

public class PestSeverity : DomainEntityId
{
        
    [MinLength(1)][MaxLength(128)]
    public string PestSeverityName { get; set; } = default!;
    
    public ICollection<Pest>? Pests { get; set; }
}