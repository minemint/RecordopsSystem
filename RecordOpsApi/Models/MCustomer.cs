namespace RecordOpsApi.Models
{
    public class MCustomer
    {
        public int customerId { get; set; }
        public string customerTitleName { get; set; } 
        public string customerFName { get; set; }
        public string customerLName { get; set; }
        public string? customerAddress { get; set; }
        public int? customerProvince { get; set; }
        public int? districtId { get; set; }
        public int? subdistrictId { get; set; }
        public string? customerPostalCode { get; set; }
        public string? customerPhone { get; set; }
        public string? customerImage { get; set; }
    }
}
