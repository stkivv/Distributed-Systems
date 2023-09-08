using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// controller for size category
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SizeCategoryController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.SizeCategoryMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public SizeCategoryController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.SizeCategoryMapper(autoMapper);
        }

        // GET: api/SizeCategorys
        /// <summary>
        /// get all size categories
        /// </summary>
        /// <returns>list of all size categories</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.SizeCategory>>> GetSizeCategorys()
        {
            var data = await 
                _bll.SizeCategoryService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/SizeCategorys/5
        /// <summary>
        /// get size category by id
        /// </summary>
        /// <param name="id">size category id</param>
        /// <returns>size category</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.SizeCategory>> GetSizeCategory(Guid id)
        {
            var sizeCategory = await _bll.SizeCategoryService.FindAsync(id);
            
            if (sizeCategory == null)
            {
                return NotFound();
            }

            return _mapper.Map(sizeCategory)!;
        }

        // PUT: api/SizeCategorys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update size category
        /// </summary>
        /// <param name="id">size category id</param>
        /// <param name="sizeCategory">size category</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSizeCategory(Guid id, Public.DTO.v1.SizeCategory sizeCategory)
        {
            if (id != sizeCategory.Id)
            {
                return BadRequest();
            }
            
            var bllSizeCategory = _mapper.Map(sizeCategory);

            _bll.SizeCategoryService.Update(bllSizeCategory!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/SizeCategorys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new size category
        /// </summary>
        /// <param name="sizeCategory">size category</param>
        /// <returns>added size category</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.SizeCategory>> PostSizeCategory(Public.DTO.v1.SizeCategory sizeCategory)
        {
            var bllSizeCategory = _mapper.Map(sizeCategory);
            _bll.SizeCategoryService.Add(bllSizeCategory!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetSizeCategory", new {id = sizeCategory.Id}, sizeCategory);

        }

        // DELETE: api/SizeCategorys/5
        /// <summary>
        /// delete size category
        /// </summary>
        /// <param name="id">size category id</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSizeCategory(Guid id)
        {
            var sizeCategory = await _bll.SizeCategoryService.RemoveAsync(id);

            if (sizeCategory == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
