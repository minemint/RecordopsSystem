namespace RecordOpsApi.Models
{
    public class MSubdistrict
    {
        public int? subdistrictId { get; set; }
        public int? provinceCode { get; set; }
        public int? districtCode { get; set; }
        public int? subdistrictCode { get; set; }
        public string? subdistrictNameEn { get; set; }
        public string? subdistrictNameTh { get; set; }
        public int? postalCode { get; set; }
        public virtual MDistrict? district { get; set; }
        public virtual MProvince? province { get; set; }

    }
}
