using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.DTO;

public class Month : DomainEntityId
{
    [Range(1, 12)]
    public int MonthNr { get; set; }
    
    [MinLength(1)][MaxLength(128)]
    public string MonthName { get; set; } = default!;
    
    public ICollection<ReminderActiveMonth>? ReminderActiveMonths { get; set; }
}