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

        [HttpPost]
        public JsonResult CreateEdit(Hospital hospital)
        {
            if (hospital.Id == 0)
            {
                _context.Hospitals.Add(hospital);
            }
            else
            {
                var existingHospital = _context.Hospitals.Find(hospital.Id);
                if (existingHospital == null)
                {
                    return new JsonResult(NotFound());
                }

                existingHospital.Name = hospital.Name;
                existingHospital.PatientsCount = hospital.PatientsCount;
                existingHospital.Description = hospital.Description;

                _context.Hospitals.Update(existingHospital);
            }

            _context.SaveChanges();
            return new JsonResult(hospital) { StatusCode = (int)HttpStatusCode.OK };
        }

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

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Hospitals.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Hospitals.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Hospitals.ToList();

            return new JsonResult(Ok(result));
        }
    }
}
