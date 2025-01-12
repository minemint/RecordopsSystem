using Microsoft.AspNetCore.Mvc.Rendering;
using RecordOps.Models;
using System.ComponentModel.DataAnnotations;

namespace RecordOps.ViewModels
{
    public class EditViewModel
    {
        [Required(ErrorMessage = "Please add a customer ID.")]
        public int? CustomerId { get; set; }
        public string? CustomerTitleName { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string? CustomerFName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string? CustomerLName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerProvince { get; set; }
        public int? DistrictId { get; set; }
        public int? SubdistrictId { get; set; }
        public string? CustomerPhone { get; set; }
        public string? customerImage { get; set; }
        public IFormFile? CustomerImage { get; set; }
        public SelectList Districts { get; set; }
        public SelectList Subdistricts { get; set; }

        public class DistrictViewModel
        {
            public int? DistrictId { get; set; }
            public string? districtName { get; set; }
        }

        public class SubdistrictViewModel
        {
            public int? SubdistrictId { get; set; }
            public string? subdistrictName { get; set; }
        }
    }
}
