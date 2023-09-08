using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Helpers.Base;
using Public.DTO.v1;

namespace WebApp.ApiControllers;

/// <summary>
/// controller that handles options when creating new plant
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OptionsForCreatePlantController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly Public.DTO.Mappers.TagMapper _tagMapper;
    private readonly Public.DTO.Mappers.SizeCategoryMapper _sizeCategoryMapper;
    private readonly Public.DTO.Mappers.PlantCollectionMapper _plantCollectionMapper;


    /// <summary>
    /// constructor that takes bll and mapper
    /// </summary>
    /// <param name="bll"></param>
    /// <param name="autoMapper"></param>
    public OptionsForCreatePlantController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _tagMapper = new Public.DTO.Mappers.TagMapper(autoMapper);
        _sizeCategoryMapper = new Public.DTO.Mappers.SizeCategoryMapper(autoMapper);
        _plantCollectionMapper = new Public.DTO.Mappers.PlantCollectionMapper(autoMapper);
    }
    
    // GET: api/OptionsForCreatePlant
    /// <summary>
    /// get all options for creating plant
    /// </summary>
    /// <returns>object with lists for each type of option </returns>
    [HttpGet]
    public async Task<ActionResult<Public.DTO.v1.OptionsForCreatePlant>> GetOptions()
    {
        var tags = (await _bll.TagService
            .AllAsync(User.GetUserId()))
            .Select(e => _tagMapper.Map(e)!)
            .ToList();

        
        var sizes = (await _bll.SizeCategoryService
            .AllAsync())
            .Select(e => _sizeCategoryMapper.Map(e)!)
            .ToList();

        var collections = (await _bll.PlantCollectionService
            .AllAsync(User.GetUserId()))
            .Select(e => _plantCollectionMapper.Map(e)!)
            .ToList();

        return new OptionsForCreatePlant()
        {
            Tags = tags,
            SizeCategories = sizes,
            PlantCollections = collections
        };
    }
    
    // POST: api/OptionsForCreatePlant
    /// <summary>
    /// makes appropriate objects based on the given data and saves them
    /// </summary>
    /// <param name="data">selected options as an object + plant id</param>
    /// <returns>OK if no problems, otherwise BadRequest</returns>
    [HttpPost]
    public async Task<IActionResult> PostOptions(OptionsForCreatePlant data)
    {
        if (data.Tags == null || data.PlantCollections == null || data.PlantId is null)
        {
            return BadRequest("dependency is null");
        }
        
        var plant = await _bll.PlantService.FindAsync(data.PlantId.Value, User.GetUserId());

        if (plant == null)
        {
            return BadRequest("plant does not exist");
        }

        //tags
        foreach (var i in plant.PlantTags!)
        {
            await _bll.PlantTagService.RemoveAsync(i.Id);
        }
        foreach (var tag in data.Tags)
        {
            var plantTag = new BLL.DTO.PlantTag()
            {
                TagId = tag.Id,
                PlantId = data.PlantId.Value
            };

            _bll.PlantTagService.Add(plantTag);
        }

        //NB!! size category should be added with the plant object, not here
        
        
        //collections
        foreach (var i in plant.PlantInCollections!)
        {
            await _bll.PlantInCollectionService.RemoveAsync(i.Id);
        }
        foreach (var plantCollection in data.PlantCollections)
        {
            var plantInCollection = new BLL.DTO.PlantInCollection()
            {
                PlantCollectionId = plantCollection.Id,
                PlantId = data.PlantId.Value
            };

            _bll.PlantInCollectionService.Add(plantInCollection);
        }

        await _bll.SaveChangesAsync();
        return Ok();

    }
}