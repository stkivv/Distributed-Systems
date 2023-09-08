using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.DTO;

public class EventType : DomainEntityId
{

    [MinLength(1)][MaxLength(128)]
    public string EventTypeName { get; set; } = default!;
    
    public ICollection<Domain.ReminderType>? ReminderTypes { get; set; }
    
    public ICollection<Domain.HistoryEntryType>? HistoryEntryTypes { get; set; }

}