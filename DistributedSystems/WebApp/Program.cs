using System.Text;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using BLL.App;
using BLL.Contracts.App;
using DAL;
using DAL.Contracts.App;
using DAL.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.App.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));


//register OUW with scoped lifecycle
builder.Services.AddScoped<IAppUOW, AppUOW>();
builder.Services.AddScoped<IAppBLL, AppBLL>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentity<AppUser, AppRole>(
        options => options.SignIn.RequireConfirmedAccount = false
    )
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    //.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services
    .AddAuthentication()
    .AddCookie(options => { options.SlidingExpiration = true; })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration.GetValue<string>("JWT:Issuer")!,
            ValidAudience = builder.Configuration.GetValue<string>("JWT:Audience")!,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:Key")!)),
            ClockSkew = TimeSpan.Zero,
        };

    });

builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsAllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

// add automapper configurations
builder.Services.AddAutoMapper(
    typeof(BLL.App.AutoMapperConfig),
    typeof(Public.DTO.AutomapperConfig)
);

var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    // in case of no explicit version
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

apiVersioningBuilder.AddApiExplorer(options =>
{
    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
    // note: the specified format code will format the version as "'v'major[.minor][-status]"
    options.GroupNameFormat = "'v'VVV";

    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

//===========================================================================================
var app = builder.Build();
//===========================================================================================


//set up database stuff and seed initial data
SetupAppData(app, app.Environment, app.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("CorsAllowAll");

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName
        );
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

//start up the web server and wait for requests
app.Run();


static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment environment, IConfiguration configuration)
{

    using var serviceScope = app.ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope();

    using var context = serviceScope.ServiceProvider
        .GetService<ApplicationDbContext>();

    if (context == null)
    {
        throw new ApplicationException("Problem in services. Can not initialize db context");
    }

    using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
    using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

    if (userManager == null || roleManager == null)
    {
        throw new ApplicationException("Problem with user manager or role manager");

    }
    
    var logger = serviceScope.ServiceProvider
        .GetService<ILogger<IApplicationBuilder>>();

    if (logger == null)
    {
        throw new ApplicationException("Problem in services. Can not initialize logger");
    }

    if (context.Database.ProviderName!.Contains("InMemory"))
    {
        return;
    }

    //WaitForDatabaseConnection(context, 0, logger);

    //drop
    if (configuration.GetValue<bool>("DataInit:DropDatabase"))
    {
        logger.LogWarning("dropping database");
        AppDataInit.DropDatabase(context);
    }
    //migrate
    if (configuration.GetValue<bool>("DataInit:MigrateDatabase"))
    {
        logger.LogInformation("migrating database");
        AppDataInit.MigrateDatabase(context);
    }
    //seed identity
    if (configuration.GetValue<bool>("DataInit:SeedIdentity"))
    {
        logger.LogInformation("seeding identity");
        AppDataInit.SeedIdentity(userManager, roleManager);
    }
    //seed application data
    if (configuration.GetValue<bool>("DataInit:SeedData"))
    {
        logger.LogInformation("seeding data");
        AppDataInit.SeedAppData(context);
    }
    
}

static void WaitForDatabaseConnection(DbContext ctx, int elapsedSeconds, ILogger logger)
{
    if (elapsedSeconds == 10)
    {
        throw new ApplicationException("Connecting to database failed.");
    }

    if (elapsedSeconds == 0)
    {
        logger.LogWarning("Connecting to the database...");
    }
    if (ctx.Database.CanConnect()) return;
    
    Thread.Sleep(1000);
    logger.LogWarning($"Retrying connection (elapsed time: {elapsedSeconds + 1}s)");
    WaitForDatabaseConnection(ctx, elapsedSeconds + 1, logger);
}

public partial class Program
{

}