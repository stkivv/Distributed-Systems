using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.DTO;

public class Photo : DomainEntityId
{
    public string ImageUrl { get; set; } = default!;
    
    [MaxLength(128)]
    public string? ImageDescription { get; set; }
    
    public Guid PlantId { get; set; }
    public Plant? Plant { get; set; }
}