using HospitalManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json; // For PostAsJsonAsync and ReadAsAsync

namespace HospitalManagement.Controllers
{
    public class HospitalController : Controller
    {
        private readonly HttpClient _httpClient;

        public HospitalController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult CreateEdit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEdit(Hospital hospital)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7082/api/HospitalManagement/CreateEdit", hospital);

            if (response.IsSuccessStatusCode)
            {
                var rawContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Raw response content: " + rawContent);

                var result = await response.Content.ReadFromJsonAsync<Hospital>();
                if (result != null)
                {
                    return RedirectToAction("Get", new { id = result.Id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Deserialization failed.");
                    return View(hospital);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to create/edit hospital.");
                return View(hospital);
            }
        }

        public async Task<IActionResult> Get(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7082/api/HospitalManagement/Get/{id}");

            if (response.IsSuccessStatusCode)
            {
                var hospital = await response.Content.ReadFromJsonAsync<Hospital>();
                if (hospital != null)
                {
                    return View(hospital);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}