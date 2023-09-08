namespace Public.DTO.v1;
/// <summary>
/// History entry. Has an entry type and is tied to a specific plant.
/// </summary>
public class HistoryEntry
{
    public Guid Id { get; set; }

    public string? EntryComment { get; set; }
    
    public DateTime EntryTime { get; set; }
    
    public HistoryEntryType? HistoryEntryType { get; set; }
    
    public Guid PlantId { get; set; }
}