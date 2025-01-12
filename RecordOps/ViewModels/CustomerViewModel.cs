using Microsoft.AspNetCore.Mvc.Rendering;
using RecordOps.Models;

namespace RecordOps.ViewModels
{
    public class CustomerViewModel
    {
        //[{"customerId":1,"customerTitleName":"mr","customerFName":"min","customerLName":"mint","customerAddress":"108/178","customerProvince":"Bangkok","districtId":1,"subdistrictId":1,"customerPostalCode":11251,"customerPhone":null,"customerImage":null,"district":{"districtId":1,"districtName":"koungtoi","subdistricts":[{"subdistrictId":1,"subdistrictName":"patumone"}]}}]
        public CustomerModel customer { get; set; }
        public DistrictModel district { get; set; }

        //public SelectList districtList { get; set; }
        //public SubdistrictModel subdistrict { get; set; }
        //public SelectList subdistrictList { get; set; }



    }
}
