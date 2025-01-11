namespace RecordOpsApi.Models
{
    public class MDistrict
    {
        public int? districtId { get; set; }
        public string? districtName { get; set; }
        public ICollection<MSubdistrict> Subdistricts { get; set; }
    }

}
