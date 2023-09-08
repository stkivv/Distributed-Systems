using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.DTO;

public class HistoryEntry : DomainEntityId
{
    [MaxLength(256)]
    public string? EntryComment { get; set; }
    
    public DateTime EntryTime { get; set; }
    
    public Guid HistoryEntryTypeId { get; set; }
    public HistoryEntryType? HistoryEntryType { get; set; }
    
    public Guid PlantId { get; set; }
    public Plant? Plant { get; set; }
}