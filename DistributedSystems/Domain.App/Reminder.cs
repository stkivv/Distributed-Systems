using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain;

public class Reminder : DomainEntityId
{
    public DateTime ReminderFrequency { get; set; }
    
    [MaxLength(256)]
    public string? ReminderMessage { get; set; }
    
    public ICollection<ReminderActiveMonth>? ReminderActiveMonths { get; set; }

    public Guid PlantId { get; set; }
    public Plant? Plant { get; set; }
    
    public Guid ReminderTypeId { get; set; }
    public ReminderType? ReminderType { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

}