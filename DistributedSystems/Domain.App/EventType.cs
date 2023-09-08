using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

public class EventType : DomainEntityId
{

    [MinLength(1)][MaxLength(128)]
    public string EventTypeName { get; set; } = default!;
    
    public ICollection<ReminderType>? ReminderTypes { get; set; }
    
    public ICollection<HistoryEntryType>? HistoryEntryTypes { get; set; }

}