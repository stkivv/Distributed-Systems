using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Public.DTO.v1;

public class EventType : DomainEntityId
{
    public string EventTypeName { get; set; } = default!;

}