namespace RecordOpsApi.Models
{
    public class MCustomer
    {
        public int? customerId { get; set; }
        public string? customerTitleName { get; set; }
        public string? customerFName { get; set; }
        public string? customerLName { get; set; }
        public string? customerAddress { get; set; }
        public int? provinceCode { get; set; }
        public int? districtCode { get; set; }
        public int? subdistrictCode { get; set; }
        public string? customerPhone { get; set; }
        public string? customerImage { get; set; }
        public virtual MDistrict? district { get; set; }
        public virtual MSubdistrict? Subdistrict { get; set; }
        public virtual MProvince? Province { get; set; }
    }
}
