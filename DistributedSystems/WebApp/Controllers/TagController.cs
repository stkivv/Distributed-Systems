using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Contracts.App;
using Domain;
using Helpers;
using Helpers.Base;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class TagController : Controller
    {
        private readonly IAppUOW _uow;
        
        public TagController(IAppUOW uow)
        {
            _uow = uow;
        }
        

        // GET: Tag
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.TagRepository.AllAsync(User.GetUserId());
            return View(vm);
        }

        // GET: Tag/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _uow.TagRepository.FindAsync(id.Value);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: Tag/Create
        public IActionResult Create()
        {
            ViewData["TagColorId"] = new SelectList(_uow.TagColorRepository.AllAsync().Result, "Id", "ColorHex");
            return View();
        }

        // POST: Tag/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TagLabel,TagColorId,AppUserId,Id")] Tag tag)
        {
            tag.AppUserId = User.GetUserId();
            if (ModelState.IsValid)
            {
                tag.Id = Guid.NewGuid();
                _uow.TagRepository.Add(tag);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TagColorId"] = new SelectList(_uow.TagColorRepository.AllAsync().Result, "Id", "ColorHex", tag.TagColorId);

            return View(tag);
        }

        // GET: Tag/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _uow.TagRepository.FindAsync(id.Value);
            if (tag == null)
            {
                return NotFound();
            }
            ViewData["TagColorId"] = new SelectList(_uow.TagColorRepository.AllAsync().Result, "Id", "ColorHex", tag.TagColorId);

            return View(tag);
        }

        // POST: Tag/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TagLabel,TagColorId,AppUserId,Id")] Tag tag)
        {
            tag.AppUserId = User.GetUserId();
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.TagRepository.Update(tag);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["TagColorId"] = new SelectList(_uow.TagColorRepository.AllAsync().Result, "Id", "ColorHex", tag.TagColorId);

            return View(tag);
        }

        // GET: Tag/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _uow.TagRepository.FindAsync(id.Value);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tag = await _uow.TagRepository.FindAsync(id);
            if (tag != null)
            {
                _uow.TagRepository.Remove(tag);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
