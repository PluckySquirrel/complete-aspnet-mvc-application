using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
	[Authorize(Roles = UserRoles.Admin)]
	public class CinemasController : Controller
    {
        private readonly ICinemasService _service;
		private readonly ILogger<CinemasController> _logger;

        public CinemasController(ICinemasService service, ILogger<CinemasController> logger)
        {
            _service = service;
			_logger = logger;
		}

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
        }
        
        //Get: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
			if(!ModelState.IsValid) return View(cinema);

			// await _service.AddAsync(cinema);
			// return RedirectToAction(nameof(Index)); 
			try
			{
				await _service.AddAsync(cinema);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				// Log the exception for debugging purposes.
				_logger.LogError(ex, "Error adding cinema to the database");
				ModelState.AddModelError(string.Empty, "An error occurred while adding the cinema.");
				return View(cinema);
			}
		}

		//Get: Cinemas/Details/1
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if(cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

		//Get: Cinemas/Edit/1
		public async Task<IActionResult> Edit(int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");
			return View(cinemaDetails);
		}

		//Get: Cinemas/Edit/1

		[HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if(!ModelState.IsValid) return View(cinema);
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

		//Get: Cinemas/Delete/1
		public async Task<IActionResult> Delete(int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");
			return View(cinemaDetails);
		}

		//Get: Cinemas/Edit/1

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirm(int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}

	}
}
    