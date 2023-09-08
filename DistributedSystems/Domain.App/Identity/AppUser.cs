using System.ComponentModel.DataAnnotations;
using Domain.Contracts.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    public ICollection<Plant>? Plants { get; set; }
    public ICollection<PlantCollection>? PlantCollections { get; set; }
    public ICollection<Reminder>? Reminders { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;

    [MaxLength(128)]
    public string LastName { get; set; } = default!;

    
    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
    
}