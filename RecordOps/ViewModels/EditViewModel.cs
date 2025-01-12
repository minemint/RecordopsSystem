using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecordOps.ViewModels
{
    public class EditViewModel
    { 
        public  int? CustomerId { get; set; } 
        public string? CustomerFName { get; set; }
        public string? CustomerLName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerProvince { get; set; }
        public int? DistrictId { get; set; }
        public int? SubdistrictId { get; set; }
        public string? CustomerPostalCode { get; set; }
        public string? CustomerPhone { get; set; }
        public string? customerImage { get; set; }
        public SelectList Districts { get; set; }
        public SelectList Subdistricts { get; set; }
    }
}
