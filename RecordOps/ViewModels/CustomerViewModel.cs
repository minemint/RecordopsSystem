using RecordOps.Models;
using System.ComponentModel.DataAnnotations;

namespace RecordOps.ViewModels
{
    public class CustomerViewModel
    {
        [Display(Name = "รหัสลูกค้า")]
        public int? customerId { get; set; }
        [Display(Name = "คำนำหน้าชื่อ")]
        public string? customerTitleName { get; set; }
        [Display(Name = "ชื่อ")]
        public string? customerFName { get; set; }
        [Display(Name = "นามสกุล")]
        public string? customerLName { get; set; }
        [Display(Name = "บ้านเลขที่")]
        public string? customerAddress { get; set; }
        [Display(Name = "จังหวัด")]
        public int? provinceCode { get; set; }
        [Display(Name = "อำเภอ")]
        public int? districtCode { get; set; }
        [Display(Name = "ตำบล")]
        public int? subdistrictCode { get; set; }
        [Display(Name = "เบอร์มือถือ")]
        public string? customerPhone { get; set; }
        public string? customerImage { get; set; }
        public DistrictModel? district { get; set; }
        public SubdistrictModel? subdistrict { get; set; }
        public ProvinceModel? province { get; set; }

    }


}
