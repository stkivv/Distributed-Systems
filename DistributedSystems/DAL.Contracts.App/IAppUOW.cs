using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IAppUOW : IBaseUOW
{
    ICollectionTypeRepository CollectionTypeRepository { get; }
    
    IHistoryEntryRepository HistoryEntryRepository { get; }
    
    IHistoryEntryTypeRepository HistoryEntryTypeRepository{ get; }
    
    IMonthRepository MonthRepository { get; }
    
    IPestRepository PestRepository { get; }
    
    IPestSeverityRepository PestSeverityRepository { get; }
    
    IPestTypeRepository PestTypeRepository { get; }
    
    IPhotoRepository PhotoRepository { get; }
    
    IPlantRepository PlantRepository { get; }
    
    IPlantCollectionRepository PlantCollectionRepository { get; }
    
    IPlantInCollectionRepository PlantInCollectionRepository { get; }
    
    IPlantTagRepository PlantTagRepository { get; }
    
    IReminderRepository ReminderRepository { get; }
    
    IReminderActiveMonthRepository ReminderActiveMonthRepository { get; }
    
    IReminderTypeRepository ReminderTypeRepository { get; }
    
    ISizeCategoryRepository SizeCategoryRepository { get; }
    
    ITagRepository TagRepository { get; }
    
    ITagColorRepository TagColorRepository { get; }
    
    IEventTypeRepository EventTypeRepository { get; }
}