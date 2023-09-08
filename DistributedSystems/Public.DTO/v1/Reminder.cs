namespace Public.DTO.v1;
/// <summary>
/// reminder for a plant.
/// </summary>
public class Reminder
{
    public Guid Id { get; set; }

    public DateTime ReminderFrequency { get; set; }
    
    public string? ReminderMessage { get; set; }
    
    public ICollection<ReminderActiveMonth>? ReminderActiveMonths { get; set; }

    public Guid PlantId { get; set; }
    public string? PlantName { get; set; }
    public ICollection<HistoryEntry>? PlantHistoryEntries { get; set; }

    public ReminderType? ReminderType { get; set; }
    
    public Guid AppUserId { get; set; }
    
    public ICollection<Month>? Months { get; set; }
}