using Microsoft.AspNetCore.Mvc;
using DAL.Contracts.App;
using Domain;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class SizeCategoryController : Controller
    {
        private readonly IAppUOW _uow;
        
        public SizeCategoryController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: SizeCategory
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.SizeCategoryRepository.AllAsync();
            return View(vm);
        }

        // GET: SizeCategory/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sizeCategory = await _uow.SizeCategoryRepository.FindAsync(id.Value);
            if (sizeCategory == null)
            {
                return NotFound();
            }

            return View(sizeCategory);
        }

        // GET: SizeCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SizeCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SizeName,Id")] SizeCategory sizeCategory)
        {
            if (ModelState.IsValid)
            {
                sizeCategory.Id = Guid.NewGuid();
                _uow.SizeCategoryRepository.Add(sizeCategory);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sizeCategory);
        }

        // GET: SizeCategory/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sizeCategory = await _uow.SizeCategoryRepository.FindAsync(id.Value);
            if (sizeCategory == null)
            {
                return NotFound();
            }
            return View(sizeCategory);
        }

        // POST: SizeCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SizeName,Id")] SizeCategory sizeCategory)
        {
            if (id != sizeCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.SizeCategoryRepository.Update(sizeCategory);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(sizeCategory);
        }

        // GET: SizeCategory/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sizeCategory = await _uow.SizeCategoryRepository.FindAsync(id.Value);
            if (sizeCategory == null)
            {
                return NotFound();
            }

            return View(sizeCategory);
        }

        // POST: SizeCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sizeCategory = await _uow.SizeCategoryRepository.FindAsync(id);
            if (sizeCategory != null)
            {
                _uow.SizeCategoryRepository.Remove(sizeCategory);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
