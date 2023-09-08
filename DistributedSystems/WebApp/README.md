cd Webapp
dotnet aspnet-codegenerator controller -m CollectionType -name CollectionTypeController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m HistoryEntry -name HistoryEntryController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m HistoryEntryType -name HistoryEntryTypeController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m Month -name MonthController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m Pest -name PestController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m PestSeverity -name PestSeverityController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m PestType -name PestTypeController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m Photo -name PhotoController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m Plant -name PlantController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m PlantCollection -name PlantCollectionController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m PlantInCollection -name PlantInCollectionController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m PlantTag -name PlantTagController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m Reminder -name ReminderController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m ReminderActiveMonth -name ReminderActiveMonthController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m ReminderType -name ReminderTypeController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m SizeCategory -name SizeCategoryController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m Tag -name TagController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m TagColor -name TagColorController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m AppUser -name AppUserController -outDir Controllers -dc ApplicationDbContext -f
dotnet aspnet-codegenerator controller -m AppRole -name AppRoleController -outDir Controllers -dc ApplicationDbContext -f


migrations
dotnet ef migrations add InitialCreate --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext
dotnet ef database update --project DAL.EF.App --startup-project WebApp
dotnet ef database drop --project DAL.EF.App --startup-project WebApp 

Generate Identity UI
cd WebApp
dotnet aspnet-codegenerator identity -dc DAL.EF.App.ApplicationDbContext --userClass AppUser -f 