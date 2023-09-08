using Domain.Base;

namespace BLL.DTO;

public class ReminderActiveMonth : DomainEntityId
{
    
    public Guid ReminderId { get; set; }
    public Reminder? Reminder { get; set; }
    
    public Guid MonthId { get; set; }
    public Month? Month { get; set; }
}