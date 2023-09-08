namespace Public.DTO.v1;
/// <summary>
/// Reminder type. Has a collection of reminders that are of this type.
/// </summary>
public class ReminderType
{
    public Guid Id { get; set; }

    public string ReminderTypeName { get; set; } = default!;
    
    public Guid EventTypeId { get; set; }
    
    //public ICollection<Reminder>? Reminders { get; set; }
}