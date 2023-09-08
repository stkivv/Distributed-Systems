using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

public class SizeCategory : DomainEntityId
{

    public ICollection<Plant>? Plants { get; set; }

    [MinLength(1)][MaxLength(128)]
    public string SizeName { get; set; } = default!;
}