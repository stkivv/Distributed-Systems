namespace Public.DTO.v1;
/// <summary>
/// Shows during which months a reminder is active.
/// </summary>
public class ReminderActiveMonth
{
    public Guid Id { get; set; }

    public Guid ReminderId { get; set; }
    
    public Guid MonthId { get; set; }
}