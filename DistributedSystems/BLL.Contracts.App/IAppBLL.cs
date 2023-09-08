using BLL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IAppBLL : IBaseBLL
{
    ICollectionTypeService CollectionTypeService { get; }
    
    IHistoryEntryService HistoryEntryService { get; }
    
    IHistoryEntryTypeService HistoryEntryTypeService { get; }
    
    IMonthService MonthService { get; }
    
    IPestService PestService { get; }
    
    IPestSeverityService PestSeverityService { get; }

    IPestTypeService PestTypeService { get; }

    IPhotoService PhotoService { get; }
    
    IPlantCollectionService PlantCollectionService { get; }
    
    IPlantInCollectionService PlantInCollectionService { get; }

    IPlantService PlantService { get; }
    
    IPlantTagService PlantTagService { get; }
    
    IReminderActiveMonthService ReminderActiveMonthService { get; }
    
    IReminderService ReminderService { get; }
    
    IReminderTypeService ReminderTypeService { get; }

    ISizeCategoryService SizeCategoryService { get; }

    ITagColorService TagColorService { get; }

    ITagService TagService { get; }
    
    IEventTypeService EventTypeService { get; }

}