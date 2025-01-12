namespace RecordOps.Models
{
    public class CustomerModel
    {
        public int? customerId { get; set; }
        public string? customerTitleName { get; set; }
        public string? customerFName { get; set; }
        public string? customerLName { get; set; }
        public string? customerAddress { get; set; }
        public string? customerProvince { get; set; }
        public int? districtId { get; set; }
        public int? subdistrictId { get; set; }
        public int? customerPostalCode { get; set; }
        public int? customerPhone { get; set; }
        public string? customerImage { get; set; }
        public virtual DistrictModel district { get; set; }
        public virtual SubdistrictModel Subdistrict { get; set; }


    }
}
