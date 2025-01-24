using Microsoft.AspNetCore.Mvc.Rendering;
using RecordOps.Models;
using System.ComponentModel.DataAnnotations;

namespace RecordOps.ViewModels
{
    public class EditViewModel
    {
        [Display(Name = "รหัสลูกค้า")]
        public int? customerId { get; set; }
        [Required]
        [Display(Name = "คำนำหน้าชื่อ")]
        public string? customerTitleName { get; set; }
        [Required]
        [Display(Name = "ชื่อ")]
        public string? customerFName { get; set; }
        [Required]
        [Display(Name = "นามสกุล")]
        public string? customerLName { get; set; }
        [Required]
        [Display(Name = "บ้านเลขที่")]
        public string? customerAddress { get; set; }
        [Required]
        [Display(Name = "จังหวัด")]
        public int? provinceCode { get; set; }
        [Required]
        [Display(Name = "อำเภอ")]
        public int? districtCode { get; set; }
        [Required]
        [Display(Name = "ตำบล")]
        public int? subdistrictCode { get; set; }
        public string? customerPhone { get; set; }
        [Required]
        [Display(Name = "รูปภาพ")]
        public string? customerImage { get; set; }
        public IFormFile? customerImageFile { get; set; }

        public SelectList? Provinces { get; set; }
        public SelectList? Districts { get; set; }
        public SelectList? Subdistricts { get; set; }

        public DistrictModel? district { get; set; }
        public SubdistrictModel? subdistrict { get; set; }
        public ProvinceModel? province { get; set; }
    }
}
