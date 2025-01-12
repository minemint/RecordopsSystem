using Microsoft.AspNetCore.Mvc.Rendering;
using RecordOps.Models;

namespace RecordOps.ViewModels
{
    public class CustomerViewModel
    {
        public int? CustomerId { get; set; }
        public string? CustomerTitleName { get; set; }
        public string? CustomerFName { get; set; }
        public string?   CustomerLName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerProvince { get; set; }
        public int? DistrictId { get; set; }
        public int? SubdistrictId { get; set; }
        public string? CustomerPostalCode { get; set; }
        public string? CustomerPhone { get; set; }
        public string? customerImage { get; set; }
        public DistrictModel District { get; set; }
        public SubdistrictModel Subdistrict { get; set; }

    }
    public class DistrictViewModel
    {
        public int? DistrictId { get; set; }
        public string? districtName { get; set; }
    }
    public class SubdistrictViewModel
    {
        public int? SubdistrictId { get; set; }
        public string? subdistrictName { get; set; }
    }



}
