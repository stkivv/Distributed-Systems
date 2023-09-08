using AutoMapper;

namespace BLL.App;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig() //between Domain and BLL
    {
        CreateMap<BLL.DTO.CollectionType, Domain.CollectionType>().ReverseMap();
        
        CreateMap<Domain.HistoryEntry,BLL.DTO.HistoryEntry>();
        
        CreateMap<BLL.DTO.HistoryEntry, Domain.HistoryEntry>()
            .ForMember(dest => dest.HistoryEntryType,
                options => options.Ignore());

        CreateMap<BLL.DTO.HistoryEntryType, Domain.HistoryEntryType>().ReverseMap();
        
        CreateMap<BLL.DTO.Month, Domain.Month>().ReverseMap();
        
        CreateMap<Domain.Pest, BLL.DTO.Pest>();
        
        CreateMap<BLL.DTO.Pest, Domain.Pest>()
            .ForMember(dest => dest.PestSeverity,
            options => options.Ignore())
            .ForMember(dest => dest.PestType,
                options => options.Ignore());

        CreateMap<BLL.DTO.PestSeverity, Domain.PestSeverity>().ReverseMap();
        
        CreateMap<BLL.DTO.PestType, Domain.PestType>().ReverseMap();

        CreateMap<BLL.DTO.Photo, Domain.Photo>().ReverseMap();

        CreateMap<BLL.DTO.Plant, Domain.Plant>()
            .ForMember(dest => dest.SizeCategory,
                options => options.Ignore());

        CreateMap<Domain.Plant, BLL.DTO.Plant>()
            .ForMember(
            dest => dest.PlantCollections,
            options => 
                options.MapFrom(src => src.PlantInCollections!.Select(e => e.PlantCollection))
            )
            .ForMember(
                dest => dest.Tags,
                options => 
                    options.MapFrom(src => src.PlantTags!.Select(e => e.Tag))
            );

        CreateMap<BLL.DTO.PlantCollection, Domain.PlantCollection>().ReverseMap();

        CreateMap<BLL.DTO.PlantInCollection, Domain.PlantInCollection>().ReverseMap();
        
        CreateMap<BLL.DTO.PlantTag, Domain.PlantTag>().ReverseMap();

        CreateMap<BLL.DTO.Reminder, Domain.Reminder>()
            .ForMember(dest => dest.ReminderType,
                options => options.Ignore());

        CreateMap<Domain.Reminder, BLL.DTO.Reminder>()
            .ForMember(
                dest => dest.Months,
                options => 
                    options.MapFrom(src => src.ReminderActiveMonths!.Select(e => e.Month))
            )
            .ForMember(
                dest => dest.PlantName,
                options => 
                    options.MapFrom(src => src.Plant!.PlantName)
            )
            .ForMember(
                dest => dest.PlantHistoryEntries,
                options => 
                    options.MapFrom(src => src.Plant!.HistoryEntries!
                        .OrderBy(e => e.EntryTime))
            );
        
        CreateMap<BLL.DTO.ReminderActiveMonth, Domain.ReminderActiveMonth>().ReverseMap();
        
        CreateMap<BLL.DTO.ReminderType, Domain.ReminderType>().ReverseMap();
        
        CreateMap<BLL.DTO.SizeCategory, Domain.SizeCategory>().ReverseMap();

        CreateMap<BLL.DTO.Tag, Domain.Tag>()
            .ForMember(dest => dest.TagColor,
                options => options.Ignore());

        CreateMap<Domain.Tag, BLL.DTO.Tag>();

        CreateMap<BLL.DTO.TagColor, Domain.TagColor>().ReverseMap();
        
        CreateMap<BLL.DTO.EventType, Domain.EventType>().ReverseMap();

    }
}