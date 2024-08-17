using System.ComponentModel.DataAnnotations;

namespace HospitalManagementAPI.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int PatientsCount { get; set; }
        public string? Description { get; set; }
    }
}
