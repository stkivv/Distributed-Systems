using DAL.Contracts.App;
using DAL.EF.Base;
using DAL.Repositories;

namespace DAL;

public class AppUOW : EFBaseUOW<ApplicationDbContext>, IAppUOW
{
    public AppUOW(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    private ICollectionTypeRepository? _collectionTypeRepository;
    public ICollectionTypeRepository CollectionTypeRepository =>
        _collectionTypeRepository ??= new CollectionTypeRepository(UowDbContext);
    
    private IHistoryEntryRepository? _historyEntryRepository;
    public IHistoryEntryRepository HistoryEntryRepository =>
        _historyEntryRepository ??= new HistoryEntryRepository(UowDbContext);
    
    private IHistoryEntryTypeRepository? _historyEntryTypeRepository;
    public IHistoryEntryTypeRepository HistoryEntryTypeRepository =>
        _historyEntryTypeRepository ??= new HistoryEntryTypeRepository(UowDbContext);
    
    private IMonthRepository? _monthRepository;
    public IMonthRepository MonthRepository =>
        _monthRepository ??= new MonthRepository(UowDbContext);
    
    private IPestRepository? _pestRepository;
    public IPestRepository PestRepository =>
        _pestRepository ??= new PestRepository(UowDbContext);
    
    private IPestSeverityRepository? _pestSeverityRepository;
    public IPestSeverityRepository PestSeverityRepository =>
        _pestSeverityRepository ??= new PestSeverityRepository(UowDbContext);
    
    private IPestTypeRepository? _pestTypeRepository;
    public IPestTypeRepository PestTypeRepository =>
        _pestTypeRepository ??= new PestTypeRepository(UowDbContext);
    
    private IPhotoRepository? _photoRepository;
    public IPhotoRepository PhotoRepository =>
        _photoRepository ??= new PhotoRepository(UowDbContext);
    
    private IPlantRepository? _plantRepository;
    public IPlantRepository PlantRepository =>
        _plantRepository ??= new PlantRepository(UowDbContext);
    
    private IPlantCollectionRepository? _plantCollectionRepository;
    public IPlantCollectionRepository PlantCollectionRepository =>
        _plantCollectionRepository ??= new PlantCollectionRepository(UowDbContext);
    
    private IPlantInCollectionRepository? _plantInCollectionRepository;
    public IPlantInCollectionRepository PlantInCollectionRepository =>
        _plantInCollectionRepository ??= new PlantInCollectionRepository(UowDbContext);
    
    private IPlantTagRepository? _plantTagRepository;
    public IPlantTagRepository PlantTagRepository =>
        _plantTagRepository ??= new PlantTagRepository(UowDbContext);
    
    private IReminderRepository? _reminderRepository;
    public IReminderRepository ReminderRepository =>
        _reminderRepository ??= new ReminderRepository(UowDbContext);
    
    private IReminderActiveMonthRepository? _reminderActiveMonthRepository;
    public IReminderActiveMonthRepository ReminderActiveMonthRepository =>
        _reminderActiveMonthRepository ??= new ReminderActiveMonthRepository(UowDbContext);
    
    private IReminderTypeRepository? _reminderTypeRepository;
    public IReminderTypeRepository ReminderTypeRepository =>
        _reminderTypeRepository ??= new ReminderTypeRepository(UowDbContext);
    
    private ISizeCategoryRepository? _sizeCategoryRepository;
    public ISizeCategoryRepository SizeCategoryRepository =>
        _sizeCategoryRepository ??= new SizeCategoryRepository(UowDbContext);
    
    private ITagRepository? _tagRepository;
    public ITagRepository TagRepository =>
        _tagRepository ??= new TagRepository(UowDbContext);
    
    private ITagColorRepository? _tagColorRepository;
    public ITagColorRepository TagColorRepository =>
        _tagColorRepository ??= new TagColorRepository(UowDbContext);

    
    private IEventTypeRepository _eventTypeRepository;
    public IEventTypeRepository EventTypeRepository =>
        _eventTypeRepository ??= new EventTypeRepository(UowDbContext);
}