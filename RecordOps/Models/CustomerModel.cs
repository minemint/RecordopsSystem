namespace RecordOps.Models
{
    public class CustomerModel
    {
        public int? customerId { get; set; }
        public string? customerTitleName { get; set; }
        public string? customerFName { get; set; }
        public string? customerLName { get; set; }
        public string? customerAddress { get; set; }
        public int? provinceCode { get; set; }
        public int? districtCode { get; set; }
        public int? subdistrictCode { get; set; }
        public string? subdistrictNameTh { get; set; }
        public string? customerPhone { get; set; }
        public string? customerImage { get; set; }
        public  DistrictModel? district { get; set; }
        public  SubdistrictModel? Subdistrict { get; set; }
        public  ProvinceModel? Province { get; set; }


    }
}
