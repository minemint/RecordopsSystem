namespace RecordOpsApi.Models
{
    public class MDistrict
    {
        public int districtId { get; set; }
        public string districtName { get; set; }
        public int provinceId { get; set; }
        public virtual ICollection<MSubdistrict> Subdistricts { get; set; }

    }

}
