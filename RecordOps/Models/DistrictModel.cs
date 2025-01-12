namespace RecordOps.Models
{
    public class DistrictModel
    {
        public int? districtId { get; set; }
        public string? districtName { get; set; }
        public int? provinceId { get; set; }
        public virtual SubdistrictModel subdistrict { get; set; }
    }
}
