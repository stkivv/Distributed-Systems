using AutoMapper;
using Domain;

namespace Public.DTO;

public class AutomapperConfig : Profile
{
    public AutomapperConfig() //between BLL and public dtos
    {
        CreateMap<BLL.DTO.CollectionType, Public.DTO.v1.CollectionType>().ReverseMap();
        
        CreateMap<BLL.DTO.HistoryEntry, Public.DTO.v1.HistoryEntry>().ReverseMap();
        
        CreateMap<BLL.DTO.HistoryEntryType, Public.DTO.v1.HistoryEntryType>().ReverseMap();
        
        CreateMap<BLL.DTO.Month, Public.DTO.v1.Month>().ReverseMap();

        CreateMap<BLL.DTO.Pest, Public.DTO.v1.Pest>().ReverseMap();

        CreateMap<BLL.DTO.PestSeverity, Public.DTO.v1.PestSeverity>().ReverseMap();
        
        CreateMap<BLL.DTO.PestType, Public.DTO.v1.PestType>().ReverseMap();

        CreateMap<BLL.DTO.Photo, Public.DTO.v1.Photo>().ReverseMap();

        CreateMap<BLL.DTO.Plant, Public.DTO.v1.Plant>().ReverseMap();

        CreateMap<BLL.DTO.PlantCollection, Public.DTO.v1.PlantCollection>().ReverseMap();

        CreateMap<BLL.DTO.PlantInCollection, Public.DTO.v1.PlantInCollection>().ReverseMap();
        
        CreateMap<BLL.DTO.PlantTag, Public.DTO.v1.PlantTag>().ReverseMap();

        CreateMap<BLL.DTO.Reminder, Public.DTO.v1.Reminder>().ReverseMap();
        
        CreateMap<BLL.DTO.ReminderActiveMonth, Public.DTO.v1.ReminderActiveMonth>().ReverseMap();
        
        CreateMap<BLL.DTO.ReminderType, Public.DTO.v1.ReminderType>().ReverseMap();
        
        CreateMap<BLL.DTO.SizeCategory, Public.DTO.v1.SizeCategory>().ReverseMap();

        CreateMap<BLL.DTO.Tag, Public.DTO.v1.Tag>().ReverseMap();

        CreateMap<BLL.DTO.TagColor, Public.DTO.v1.TagColor>().ReverseMap();
        
        CreateMap<BLL.DTO.EventType, Public.DTO.v1.EventType>().ReverseMap();

    }
    
}