using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Contracts.App;
using Domain;
using Helpers;
using Helpers.Base;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class PlantTagController : Controller
    {
        private readonly IAppUOW _uow;
        
        public PlantTagController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: PlantTag
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.PlantTagRepository.AllAsync(User.GetUserId());
            return View(vm);
        }

        // GET: PlantTag/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantTag = await _uow.PlantTagRepository.FindAsync(id.Value);
            if (plantTag == null)
            {
                return NotFound();
            }

            return View(plantTag);
        }

        // GET: PlantTag/Create
        public IActionResult Create()
        {
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync(User.GetUserId()).Result, "Id", "PlantName");
            ViewData["TagId"] = new SelectList(_uow.TagRepository.AllAsync(User.GetUserId()).Result, "Id", "TagLabel");
            return View();
        }

        // POST: PlantTag/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TagId,PlantId,Id")] PlantTag plantTag)
        {
            if (ModelState.IsValid)
            {
                plantTag.Id = Guid.NewGuid();
                _uow.PlantTagRepository.Add(plantTag);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync(User.GetUserId()).Result, "Id", "PlantName", plantTag.PlantId);
            ViewData["TagId"] = new SelectList(_uow.TagRepository.AllAsync(User.GetUserId()).Result, "Id", "TagLabel", plantTag.TagId);
            return View(plantTag);
        }

        // GET: PlantTag/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantTag = await _uow.PlantTagRepository.FindAsync(id.Value);
            if (plantTag == null)
            {
                return NotFound();
            }
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync(User.GetUserId()).Result, "Id", "PlantName", plantTag.PlantId);
            ViewData["TagId"] = new SelectList(_uow.TagRepository.AllAsync(User.GetUserId()).Result, "Id", "TagLabel", plantTag.TagId);
            return View(plantTag);
        }

        // POST: PlantTag/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TagId,PlantId,Id")] PlantTag plantTag)
        {
            if (id != plantTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PlantTagRepository.Update(plantTag);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync(User.GetUserId()).Result, "Id", "PlantName", plantTag.PlantId);
            ViewData["TagId"] = new SelectList(_uow.TagRepository.AllAsync(User.GetUserId()).Result, "Id", "TagLabel", plantTag.TagId);
            return View(plantTag);
        }

        // GET: PlantTag/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantTag = await _uow.PlantTagRepository.FindAsync(id.Value);
            if (plantTag == null)
            {
                return NotFound();
            }

            return View(plantTag);
        }

        // POST: PlantTag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var plantTag = await _uow.PlantTagRepository.FindAsync(id);
            if (plantTag != null)
            {
                _uow.PlantTagRepository.Remove(plantTag);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
