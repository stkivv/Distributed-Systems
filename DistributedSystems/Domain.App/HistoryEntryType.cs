using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain;

public class HistoryEntryType : DomainEntityId
{

    [MinLength(1)][MaxLength(128)]
    public string EntryTypeName { get; set; } = default!;
    
    public ICollection<HistoryEntry>? HistoryEntries { get; set; }
    
    public Guid EventTypeId { get; set; }
    public EventType? EventType { get; set; }
    
}