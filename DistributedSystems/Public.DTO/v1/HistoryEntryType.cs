namespace Public.DTO.v1;
/// <summary>
/// History entry type. Has a list of history entries that use said type.
/// </summary>
public class HistoryEntryType
{
    public Guid Id { get; set; }
    
    public string EntryTypeName { get; set; } = default!;
    
    public Guid EventTypeId { get; set; }
}