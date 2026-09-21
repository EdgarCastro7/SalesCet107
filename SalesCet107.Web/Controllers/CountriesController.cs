using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using SalesCet107.Web.Data;
using SalesCet107.Web.Data.Entities;
using SkiaSharp;

namespace SalesCet107.Web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly DataContext _context;

        public CountriesController(ICountryRepository countryRepository, DataContext context)
        {
            _countryRepository = countryRepository;
            _context = context;
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

            bool nameExists = await _context.Countries
          .AnyAsync(c => c.Name.ToLower() == country.Name.ToLower() && c.Id != country.Id);

            if (nameExists)
            {
                ModelState.AddModelError("Name", "This country is already registered.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                await _countryRepository.UpdateAsync(country);

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
