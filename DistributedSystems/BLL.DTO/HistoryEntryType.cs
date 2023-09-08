using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Domain.Contracts.Base;

namespace BLL.DTO;

public class HistoryEntryType : DomainEntityId
{
    [MinLength(1)][MaxLength(128)]
    public string EntryTypeName { get; set; } = default!;
    
    public ICollection<HistoryEntry>? HistoryEntries { get; set; }
    
    public Guid EventTypeId { get; set; }
}