using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Helpers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// controller for tag
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TagController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.TagMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public TagController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.TagMapper(autoMapper);
        }

        // GET: api/Tags
        /// <summary>
        /// get all tags
        /// </summary>
        /// <returns>list of tags</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Tag>>> GetTags()
        {
            var data = await 
                _bll.TagService.AllAsync(User.GetUserId());

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/Tags/5
        /// <summary>
        /// get tag by id
        /// </summary>
        /// <param name="id">tag id</param>
        /// <returns>tag</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.Tag>> GetTag(Guid id)
        {
            var tag = await _bll.TagService.FindAsync(id, User.GetUserId());
            
            if (tag == null)
            {
                return NotFound();
            }

            return _mapper.Map(tag)!;
        }

        // PUT: api/Tags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update tag
        /// </summary>
        /// <param name="id">tag id</param>
        /// <param name="tag">tag</param>
        /// <returns>noContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(Guid id, Public.DTO.v1.Tag tag)
        {
            if (id != tag.Id)
            {
                return BadRequest();
            }
            
            var bllTag = _mapper.Map(tag);
            bllTag!.AppUserId = User.GetUserId();
            _bll.TagService.Update(bllTag);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/Tags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <returns>added tag</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.Tag>> PostTag(Public.DTO.v1.Tag tag)
        {
            var bllTag = _mapper.Map(tag);
            bllTag!.AppUserId = User.GetUserId();
            _bll.TagService.Add(bllTag);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTag", new {id = tag.Id}, tag);

        }

        // DELETE: api/Tags/5
        /// <summary>
        /// delete a tag
        /// </summary>
        /// <param name="id">tag id</param>
        /// <returns>noContent</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            var tag = await _bll.TagService.RemoveAsync(id, User.GetUserId());

            if (tag == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
