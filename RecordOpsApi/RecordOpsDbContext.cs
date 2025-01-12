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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { // กำหนด Primary Key
            modelBuilder.Entity<MCustomer>().HasKey(x => x.customerId);
            modelBuilder.Entity<MDistrict>().HasKey(x => x.districtId);
            modelBuilder.Entity<MSubdistrict>().HasKey(x => x.subdistrictId);

            modelBuilder.Entity<MCustomer>().HasOne(d => d.district)
                .WithMany()
                .HasForeignKey(d => d.districtId);

            modelBuilder.Entity<MDistrict>()
        .HasMany(d => d.Subdistricts)
        .WithOne()
        .HasForeignKey(s => s.subdistrictId);
            
                
            

            
            // Mapping ชื่อตาราง (Optional)
            modelBuilder.Entity<MCustomer>().ToTable("customer_tbl");
            modelBuilder.Entity<MDistrict>().ToTable("district_tbl");
            modelBuilder.Entity<MSubdistrict>().ToTable("subdistrict_tbl");
        }
    }
}
