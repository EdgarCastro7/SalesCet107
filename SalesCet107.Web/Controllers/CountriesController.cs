using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using SalesCet107.Web.Data;
using SalesCet107.Web.Data.Entities;

namespace SalesCet107.Web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountryRepository _countryRepository;

        public CountriesController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public IActionResult Index()
        {
            var countries = _countryRepository.GetAll();

            return View(countries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                var allCountries = _countryRepository.GetAll();

                if (allCountries.Any(e => e.Name == country.Name))
                {
                    ModelState.AddModelError("Name", "A country with that name already exists");

                    return View(country);
                }

                await _countryRepository.CreateAsync(country);

                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        public async Task<IActionResult> Details(int id)
        {
            var country = await _countryRepository.GetByIdAsync(id);

            if(country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var country = await _countryRepository.GetByIdAsync(id.Value);

            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var allCountries = _countryRepository.GetAll();

                if(allCountries.Any(e => e.Name == country.Name && e.Id != country.Id))
                {
                    ModelState.AddModelError("Name", "A country with that name already exists");

                    return View(country);
                }

                await _countryRepository.UpdateAsync(country);

                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _countryRepository.GetByIdAsync(id.Value);

            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var country = await _countryRepository.GetByIdAsync(id);

            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _countryRepository.DeleteAsync(country);

                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }
    }
}
