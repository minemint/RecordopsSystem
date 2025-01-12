using Microsoft.AspNetCore.Mvc.Rendering;
using RecordOps.Models;
using System.ComponentModel.DataAnnotations;

namespace RecordOps.ViewModels
{
    public class CustomerViewModel
    {
        [Required]

        public int? CustomerId { get; set; }
        [Required]

        public string? CustomerTitleName { get; set; }
        [Required]
        public string? CustomerFName { get; set; }
        [Required]
        public string?   CustomerLName { get; set; }
        [Required]
        public string? CustomerAddress { get; set; }
        [Required]
        public string? CustomerProvince { get; set; }
        [Required]

        public int? DistrictId { get; set; }
        [Required]

        public int? SubdistrictId { get; set; }
        [Required]
        public string? CustomerPostalCode { get; set; }
        [Required]
        public string? CustomerPhone { get; set; }
        
        public string? customerImage { get; set; }
        public DistrictModel District { get; set; }
        public SubdistrictModel Subdistrict { get; set; }

    }
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
