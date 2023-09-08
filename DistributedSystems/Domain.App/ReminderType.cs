using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

public class ReminderType : DomainEntityId
{

    [MinLength(1)][MaxLength(128)]
    public string ReminderTypeName { get; set; } = default!;
    
    public ICollection<Reminder>? Reminders { get; set; }
    
    public Guid EventTypeId { get; set; }
    public EventType? EventType { get; set; }

}