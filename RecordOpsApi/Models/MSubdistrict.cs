namespace RecordOpsApi.Models
{
    public class MSubdistrict
    {
        public int subdistrictId { get; set; }
        public string subdistrictName { get; set; }
        public int districtId { get; set; }
        public virtual MDistrict District { get; set; }

    }
}
