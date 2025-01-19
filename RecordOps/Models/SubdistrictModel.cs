namespace RecordOps.Models
{
    public class SubdistrictModel
    {
        public int? subdistrictId { get; set; }
        public int? provinceCode { get; set; }
        public int? districtCode { get; set; }
        public int? subdistrictCode { get; set; }
        public string? subdistrictNameEn { get; set; }
        public string? subdistrictNameTh { get; set; }
        public int? postalCode { get; set; }
        public DistrictModel? district { get; set; }
        public ProvinceModel? Province { get; set; }
    }
}
