using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// controller for tag color
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TagColorController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.TagColorMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public TagColorController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.TagColorMapper(autoMapper);
        }

        // GET: api/TagColors
        /// <summary>
        /// get all tag colors
        /// </summary>
        /// <returns>list of tag colors</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.TagColor>>> GetTagColors()
        {
            var data = await 
                _bll.TagColorService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/TagColors/5
        /// <summary>
        /// get tag color by id
        /// </summary>
        /// <param name="id">tag color id</param>
        /// <returns>tag color</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.TagColor>> GetTagColor(Guid id)
        {
            var tagColor = await _bll.TagColorService.FindAsync(id);
            
            if (tagColor == null)
            {
                return NotFound();
            }

            return _mapper.Map(tagColor)!;
        }

        // PUT: api/TagColors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update tag color
        /// </summary>
        /// <param name="id">tag color id</param>
        /// <param name="tagColor">tag color</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTagColor(Guid id, Public.DTO.v1.TagColor tagColor)
        {
            if (id != tagColor.Id)
            {
                return BadRequest();
            }
            
            var bllTagColor = _mapper.Map(tagColor);

            _bll.TagColorService.Update(bllTagColor!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/TagColors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new tag color
        /// </summary>
        /// <param name="tagColor">tag color</param>
        /// <returns>added tag color</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.TagColor>> PostTagColor(Public.DTO.v1.TagColor tagColor)
        {
            var bllTagColor = _mapper.Map(tagColor);
            _bll.TagColorService.Add(bllTagColor!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTagColor", new {id = tagColor.Id}, tagColor);

        }

        // DELETE: api/TagColors/5
        /// <summary>
        /// delete tag color
        /// </summary>
        /// <param name="id">tag color id</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTagColor(Guid id)
        {
            var tagColor = await _bll.TagColorService.RemoveAsync(id);

            if (tagColor == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
