namespace RecordOpsApi.Models
{
    public class MDistrict
    {
        public int? districtId { get; set; }
        public int? districtCode { get; set; }
        public string? districtNameEn { get; set; }
        public string? districtNameTh { get; set; }
        public int? postalCode { get; set; }
        public int? provinceCode { get; set; }
        public virtual MProvince? province { get; set; }



    }

}
