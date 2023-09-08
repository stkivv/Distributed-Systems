using AutoMapper;
using BLL.App.Mappers;
using BLL.App.services;
using BLL.Base;
using BLL.Contracts.App;
using DAL.Contracts.App;

namespace BLL.App;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    protected new readonly IAppUOW Uow;
    private readonly AutoMapper.IMapper _mapper;

    public AppBLL(IAppUOW uow, IMapper mapper) : base(uow)
    {
        Uow = uow;
        _mapper = mapper;
    }

    private ICollectionTypeService? _collectionTypes;
    public ICollectionTypeService CollectionTypeService =>
        _collectionTypes ??= new CollectionTypeService(Uow, new CollectionTypeMapper(_mapper));
    
    
    private IHistoryEntryService? _historyEntries;
    public IHistoryEntryService HistoryEntryService =>
        _historyEntries ??= new HistoryEntryService(Uow, new HistoryEntryMapper(_mapper));
    
    
    private IHistoryEntryTypeService? _historyEntryType;
    public IHistoryEntryTypeService HistoryEntryTypeService =>
        _historyEntryType ??= new HistoryEntryTypeService(Uow, new HistoryEntryTypeMapper(_mapper));

    
    private IMonthService? _month;
    public IMonthService MonthService =>
        _month ??= new MonthService(Uow, new MonthMapper(_mapper));

    
    private IPestService? _pest;
    public IPestService PestService =>
        _pest ??= new PestService(Uow, new PestMapper(_mapper));
    
        
    private IPestSeverityService? _pestSeverity;
    public IPestSeverityService PestSeverityService =>
        _pestSeverity ??= new PestSeverityService(Uow, new PestSeverityMapper(_mapper));
    
        
    private IPestTypeService? _pestType;
    public IPestTypeService PestTypeService =>
        _pestType ??= new PestTypeService(Uow, new PestTypeMapper(_mapper));
        
    
    private IPhotoService? _photo;
    public IPhotoService PhotoService =>
        _photo ??= new PhotoService(Uow, new PhotoMapper(_mapper));
        
    
    private IPlantCollectionService? _plantCollection;
    public IPlantCollectionService PlantCollectionService =>
        _plantCollection ??= new PlantCollectionService(Uow, new PlantCollectionMapper(_mapper));
    
        
    private IPlantInCollectionService? _plantInCollection;
    public IPlantInCollectionService PlantInCollectionService =>
        _plantInCollection ??= new PlantInCollectionService(Uow, new PlantInCollectionMapper(_mapper));
        
    
    private IPlantService? _plant;
    public IPlantService PlantService =>
        _plant ??= new PlantService(Uow, new PlantMapper(_mapper));
    
        
    private IPlantTagService? _plantTag;
    public IPlantTagService PlantTagService =>
        _plantTag ??= new PlantTagService(Uow, new PlantTagMapper(_mapper));
    
        
    private IReminderActiveMonthService? _reminderActiveMonth;
    public IReminderActiveMonthService ReminderActiveMonthService =>
        _reminderActiveMonth ??= new ReminderActiveMonthService(Uow, new ReminderActiveMonthMapper(_mapper));
    
        
    private IReminderService? _reminder;
    public IReminderService ReminderService =>
        _reminder ??= new ReminderService(Uow, new ReminderMapper(_mapper));
    
        
    private IReminderTypeService? _reminderType;
    public IReminderTypeService ReminderTypeService =>
        _reminderType ??= new ReminderTypeService(Uow, new ReminderTypeMapper(_mapper));
    
        
    private ISizeCategoryService? _sizeCategory;
    public ISizeCategoryService SizeCategoryService =>
        _sizeCategory ??= new SizeCategoryService(Uow, new SizeCategoryMapper(_mapper));
    
        
    private ITagColorService? _tagColor;
    public ITagColorService TagColorService =>
        _tagColor ??= new TagColorService(Uow, new TagColorMapper(_mapper));
    
        
    private ITagService? _tag;
    public ITagService TagService =>
        _tag ??= new TagService(Uow, new TagMapper(_mapper));
    
    
    private IEventTypeService? _event;
    public IEventTypeService EventTypeService =>
        _event ??= new EventTypeService(Uow, new EventTypeMapper(_mapper));
}