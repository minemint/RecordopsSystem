using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecordOps.Models;
using System.ComponentModel.DataAnnotations;

namespace RecordOps.ViewModels
{
    public class CreateViewModel
    {
        [Display(Name = "รหัสลูกค้า")]
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
        [Display(Name = "เบอร์มือถือ")]
        public string? customerPhone { get; set; }
        [Display(Name = "รูปภาพ")]
        public string? customerImage { get; set; }

        public string? provinceNameTh { get; set; }
        public string? provinceNameEn { get; set; }
        public string? districtNameEn { get; set; }
        public string? districtNameTh { get; set; }
        public string? subdistrictNameTh { get; set; }
        public string? subdistrictNameEn { get; set; }
        public int? provinceCode { get; set; }
        [Required]
        [Display(Name = "เขต")]
        public int? districtCode { get; set; }
        [Required]
        [Display(Name = "แขวง")]
        public int? subdistrictCode { get; set; }
        //public string? districtNameTh { get; set; }
        //public string? districtNameEn { get; set; }
        //public string? subdistrictNameTh { get; set; }
        //public string? subdistrictNameEn { get; set; }
        //public string? provinceNameTh { get; set; }
        //public string? provinceNameEn { get; set; }

        public IFormFile? customerImageFile { get; set; }
        public SelectList? Districts { get; set; }
        public SelectList? Subdistricts { get; set; }
        public SelectList? Provinces { get; set; }

        public DistrictModel? district { get; set; }
        public SubdistrictModel? subdistrict { get; set; }
        public ProvinceModel? province { get; set; }

    }
}
