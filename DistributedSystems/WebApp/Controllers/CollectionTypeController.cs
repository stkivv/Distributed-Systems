using Microsoft.AspNetCore.Mvc;
using DAL.Contracts.App;
using Domain;
using Microsoft.AspNetCore.Authorization;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    [Authorize]
    public class CollectionTypeController : Controller
    {
        private readonly IAppUOW _uow;


        public CollectionTypeController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: CollectionType
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.CollectionTypeRepository.AllAsync();
            return View(vm);
        }

        // GET: CollectionType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectionType = await _uow.CollectionTypeRepository.FindAsync(id.Value);
            if (collectionType == null)
            {
                return NotFound();
            }

            return View(collectionType);
        }

        // GET: CollectionType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CollectionType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CollectionTypeName,Id")] CollectionType collectionType)
        {
            if (ModelState.IsValid)
            {
                collectionType.Id = Guid.NewGuid();
                _uow.CollectionTypeRepository.Add(collectionType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collectionType);
        }

        // GET: CollectionType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectionType = await _uow.CollectionTypeRepository.FindAsync(id.Value);
            if (collectionType == null)
            {
                return NotFound();
            }
            return View(collectionType);
        }

        // POST: CollectionType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CollectionTypeName,Id")] CollectionType collectionType)
        {
            if (id != collectionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.CollectionTypeRepository.Update(collectionType);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(collectionType);
        }

        // GET: CollectionType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collectionType = await _uow.CollectionTypeRepository.FindAsync(id.Value);
            if (collectionType == null)
            {
                return NotFound();
            }

            return View(collectionType);
        }

        // POST: CollectionType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var collectionType = await _uow.CollectionTypeRepository.FindAsync(id);
            if (collectionType != null)
            {
                _uow.CollectionTypeRepository.Remove(collectionType);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
