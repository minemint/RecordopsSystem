using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RecordOps.ViewModels
{
    public class CreateViewModel
    {
        [Required(ErrorMessage = "Please add a name.")]
        public string? CustomerTitleName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string? CustomerFName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string? CustomerLName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string? CustomerAddress { get; set; }

        [Required(ErrorMessage = "Province is required.")]
        public string? CustomerProvince { get; set; }

        [Required(ErrorMessage = "District is required.")]
        public int? DistrictId { get; set; }

        [Required(ErrorMessage = "Subdistrict is required.")]
        public int? SubdistrictId { get; set; }

        [Required(ErrorMessage = "Postal code is required.")]
        public string? CustomerPostalCode { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public int? CustomerPhone { get; set; }

        public IFormFile? CustomerImage { get; set; }
        public SelectList? Districts { get; set; }
        public SelectList? Subdistricts { get; set; }
    }
}
