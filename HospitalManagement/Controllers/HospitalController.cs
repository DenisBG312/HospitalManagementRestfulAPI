using Microsoft.AspNetCore.Mvc;
using HospitalManagementAPI.Models;
using System.Net.Http.Json;

namespace HospitalManagement.Controllers
{
    public class HospitalController : Controller
    {
        private readonly HttpClient _httpClient;

        public HospitalController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> GetAll()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Hospital>>("https://localhost:7082/api/HospitalManagement/GetAll");

            if (response != null)
            {
                return View(response);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Get(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Hospital>($"https://localhost:7082/api/HospitalManagement/Get/{id}");

            if (response != null)
            {
                return View(response);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: /Hospital/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Hospital/Create
        [HttpPost]
        public async Task<IActionResult> Create(Hospital hospital)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7082/api/HospitalManagement/Create", hospital);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to create hospital.");
                return View(hospital);
            }
        }

        // GET: /Hospital/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Hospital>($"https://localhost:7082/api/HospitalManagement/Get/{id}");

            if (response != null)
            {
                return View(response);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: /Hospital/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Hospital hospital)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7082/api/HospitalManagement/Edit/{hospital.Id}", hospital);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to edit hospital.");
                return View(hospital);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7082/api/HospitalManagement/Delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
