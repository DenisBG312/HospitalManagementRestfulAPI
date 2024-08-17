using HospitalManagementAPI.Data;
using HospitalManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HospitalManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HospitalManagementController : ControllerBase
    {
        private readonly ApiContext _context;
        public HospitalManagementController(ApiContext context)
        {
            _context = context;
        }

        // Create a new hospital
        [HttpPost]
        public IActionResult Create([FromBody] Hospital hospital)
        {
            if (hospital == null)
            {
                return BadRequest("Hospital data is null.");
            }

            _context.Hospitals.Add(hospital);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = hospital.Id }, hospital);
        }

        // Update an existing hospital
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Hospital hospital)
        {
            if (hospital == null || hospital.Id != id)
            {
                return BadRequest("Hospital data is invalid.");
            }

            var existingHospital = _context.Hospitals.Find(id);
            if (existingHospital == null)
            {
                return NotFound();
            }

            existingHospital.Name = hospital.Name;
            existingHospital.PatientsCount = hospital.PatientsCount;
            existingHospital.Description = hospital.Description;

            _context.Hospitals.Update(existingHospital);
            _context.SaveChanges();

            return Ok(existingHospital);
        }

        // Get a hospital by ID
        [HttpGet("{id}")]
        public ActionResult<Hospital> Get(int id)
        {
            var result = _context.Hospitals.Find(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // Delete a hospital by ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _context.Hospitals.Find(id);

            if (result == null)
            {
                return NotFound();
            }

            _context.Hospitals.Remove(result);
            _context.SaveChanges();

            return NoContent();
        }

        // Get all hospitals
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _context.Hospitals.ToList();
            return Ok(result);
        }
    }
}
