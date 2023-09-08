using Microsoft.AspNetCore.Mvc;
using DAL.Contracts.App;
using Domain;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class TagColorController : Controller
    {
        private readonly IAppUOW _uow;

        public TagColorController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: TagColor
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.TagColorRepository.AllAsync();
            return View(vm);
        }

        // GET: TagColor/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagColor = await _uow.TagColorRepository.FindAsync(id.Value);
            if (tagColor == null)
            {
                return NotFound();
            }

            return View(tagColor);
        }

        // GET: TagColor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TagColor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColorName,ColorHex,Id")] TagColor tagColor)
        {
            if (ModelState.IsValid)
            {
                tagColor.Id = Guid.NewGuid();
                _uow.TagColorRepository.Add(tagColor);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tagColor);
        }

        // GET: TagColor/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagColor = await _uow.TagColorRepository.FindAsync(id.Value);
            if (tagColor == null)
            {
                return NotFound();
            }
            return View(tagColor);
        }

        // POST: TagColor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ColorName,ColorHex,Id")] TagColor tagColor)
        {
            if (id != tagColor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.TagColorRepository.Update(tagColor);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tagColor);
        }

        // GET: TagColor/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagColor = await _uow.TagColorRepository.FindAsync(id.Value);
            if (tagColor == null)
            {
                return NotFound();
            }

            return View(tagColor);
        }

        // POST: TagColor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tagColor = await _uow.TagColorRepository.FindAsync(id);
            if (tagColor != null)
            {
                _uow.TagColorRepository.Remove(tagColor);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
