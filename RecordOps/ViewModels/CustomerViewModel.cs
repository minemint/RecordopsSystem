using RecordOps.Models;

namespace RecordOps.ViewModels
{
    public class CustomerViewModel
    {
        public int? customerId { get; set; }
        public string? customerTitleName { get; set; }
        public string? customerFName { get; set; }
        public string? customerLName { get; set; }
        public string? customerAddress { get; set; }
        public string? customerPhone { get; set; }
        public string? customerImage { get; set; }
        public string? districtNameTh { get; set; }
        public int? postalCode { get; set; }
        public string? provinceNameTh { get; set; }
        public string? subdistrictNameTh { get; set; }

    }


}
