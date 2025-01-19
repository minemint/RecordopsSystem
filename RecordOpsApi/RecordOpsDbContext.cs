using Microsoft.EntityFrameworkCore;
using RecordOpsApi.Models;

namespace RecordOpsApi
{
    public class RecordOpsDbContext : DbContext
    {
        public RecordOpsDbContext(DbContextOptions options) : base(options)
        {

        }
        //customer_tbl is the table name
        public DbSet<MCustomer> customer_tbl { get; set; }
        public DbSet<MDistrict> district_tbl { get; set; }
        public DbSet<MSubdistrict> subdistrict_tbl { get; set; }
        public DbSet<MProvince> province_tbl { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MCustomer>().HasKey(x => x.customerId);
            modelBuilder.Entity<MDistrict>().HasKey(x => x.districtCode);
            modelBuilder.Entity<MSubdistrict>().HasKey(x => x.subdistrictCode);
            modelBuilder.Entity<MProvince>().HasKey(x => x.provinceCode);


            // ความสัมพันธ์ระหว่าง MCustomer กับ MDistrict
            modelBuilder.Entity<MCustomer>()
                .HasOne(c => c.district)
                .WithMany()
                .HasForeignKey(c => c.districtCode);

            // ความสัมพันธ์ระหว่าง MCustomer กับ MSubdistrict
            modelBuilder.Entity<MCustomer>()
                .HasOne(c => c.Subdistrict)
                .WithMany()
                .HasForeignKey(c => c.subdistrictCode);

            // ความสัมพันธ์ระหว่าง MCustomer กับ MProvince
            modelBuilder.Entity<MCustomer>()
                .HasOne(c => c.Province)
                .WithMany()
                .HasForeignKey(c => c.provinceCode);

            // ความสัมพันธ์ระหว่าง MSubdistrict กับ MDistrict
            modelBuilder.Entity<MSubdistrict>()
                .HasOne(sd => sd.district)
                .WithMany()
                .HasForeignKey(sd => sd.districtCode);

            // ความสัมพันธ์ระหว่าง MSubdistrict กับ MProvince
            modelBuilder.Entity<MSubdistrict>()
                .HasOne(sd => sd.province)
                .WithMany()
                .HasForeignKey(sd => sd.provinceCode);

            // ความสัมพันธ์ระหว่าง Mdistrict กับ MProvince
            modelBuilder.Entity<MDistrict>()
                .HasOne(sd => sd.province)
                .WithMany()
                .HasForeignKey(sd => sd.provinceCode);



            // Mapping ชื่อตาราง (Optional)
            modelBuilder.Entity<MProvince>().ToTable("province_tbl");
            modelBuilder.Entity<MCustomer>().ToTable("customer_tbl");
            modelBuilder.Entity<MDistrict>().ToTable("district_tbl");
            modelBuilder.Entity<MSubdistrict>().ToTable("subdistrict_tbl");
        }
    }
}
