using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RecordOps.ViewModels
{
    public class CreateViewModel
    {
        public string? customerTitleName { get; set; }
        public string? customerFName { get; set; }
        public string? customerLName { get; set; }
        public string? customerAddress { get; set; }
        public string? customerPhone { get; set; }
        public string? customerImage { get; set; }
        public int? districtCode { get; set; }
        public int? subdistrictCode { get; set; }
        public int? provinceCode { get; set; }
        public string? districtNameTh { get; set; }
        public string? subdistrictNameTh { get; set; }
        public string? provinceNameTh { get; set; }

        public IFormFile? CustomerImage { get; set; }
        public SelectList? Districts { get; set; }
        public SelectList? Subdistricts { get; set; }

    }
}
