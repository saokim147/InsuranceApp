using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InsuranceWebApp.Data
{
    public partial class HospitalDbContext : IdentityDbContext<ApplicationUser>
    {
        public HospitalDbContext()
        {

        }
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<District> Districts { get; set; }

        public virtual DbSet<Hospital> Hospitals { get; set; }

        public virtual DbSet<Ward> Wards { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var admin = new IdentityRole("Admin")
            {
                NormalizedName = "Admin"
            };
            var superAdmin = new IdentityRole("SuperAdmin")
            {
                NormalizedName = "SuperAdmin"
            };

            builder.Entity<IdentityRole>().HasData(admin, superAdmin);
            builder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CityId).HasName("PK__Cities__6E64DFEA4D3DD7A5");

                entity.Property(e => e.CityId).HasColumnName("CITY_ID");
                entity.Property(e => e.CityName)
                    .HasMaxLength(50)
                    .HasColumnName("CITY_NAME");
            });

            builder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.DistrictId).HasName("PK__District__F24D1D90912228B5");

                entity.Property(e => e.DistrictId).HasColumnName("DISTRICT_ID");
                entity.Property(e => e.CityId).HasColumnName("CITY_ID");
                entity.Property(e => e.DistrictName)
                    .HasMaxLength(50)
                    .HasColumnName("DISTRICT_NAME");

                entity.HasOne(d => d.City).WithMany(p => p.Districts)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__Districts__CITY___02FC7413");
            });

            builder.Entity<Hospital>(entity =>
            {
                entity.HasKey(e => e.HospitalId).HasName("PK__Hospital__AAE760882E76082B");

                entity.Property(e => e.HospitalId).HasColumnName("HOSPITAL_ID");
                entity.Property(e => e.BillingTime)
                    .HasMaxLength(100)
                    .HasColumnName("BILLING_TIME");
                entity.Property(e => e.CityId).HasColumnName("CITY_ID");
                entity.Property(e => e.Dental).HasColumnName("DENTAL");
                entity.Property(e => e.DistrictId).HasColumnName("DISTRICT_ID");
                entity.Property(e => e.HospitalAddress)
                    .HasMaxLength(150)
                    .HasColumnName("HOSPITAL_ADDRESS");
                entity.Property(e => e.HospitalName)
                    .HasMaxLength(100)
                    .HasColumnName("HOSPITAL_NAME");
                entity.Property(e => e.InPatient).HasColumnName("IN_PATIENT");
                entity.Property(e => e.InsuranceAndDirectBilling)
                    .HasMaxLength(100)
                    .HasColumnName("INSURANCE_AND_DIRECT_BILLING");
                entity.Property(e => e.IsPublicHospital).HasColumnName("IS_PUBLIC_HOSPITAL");
                entity.Property(e => e.Latitude)
                    .HasColumnType("float")
                    .HasColumnName("LATITUDE");
                entity.Property(e => e.Longitude)
                    .HasColumnType("float")
                    .HasColumnName("LONGITUDE");
                entity.Property(e => e.Note)
                    .HasMaxLength(550)
                    .HasColumnName("NOTE");
                entity.Property(e => e.OutPatient).HasColumnName("OUT_PATIENT");
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(100)
                    .HasColumnName("PHONE_NUMBER");
                entity.Property(e => e.WardId).HasColumnName("WARD_ID");

                entity.HasOne(d => d.City).WithMany(p => p.Hospitals)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__Hospitals__CITY___06CD04F7");

                entity.HasOne(d => d.District).WithMany(p => p.Hospitals)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__Hospitals__DISTR__07C12930");

                entity.HasOne(d => d.Ward).WithMany(p => p.Hospitals)
                    .HasForeignKey(d => d.WardId)
                    .HasConstraintName("FK__Hospitals__WARD___08B54D69");
            });

            builder.Entity<Ward>(entity =>
            {
                entity.HasKey(e => e.WardId).HasName("PK__Wards__313533870E9B7A5A");

                entity.Property(e => e.WardId).HasColumnName("WARD_ID");
                entity.Property(e => e.DistrictId).HasColumnName("DISTRICT_ID");
                entity.Property(e => e.WardName)
                    .HasMaxLength(50)
                    .HasColumnName("WARD_NAME");

                entity.HasOne(d => d.District).WithMany(p => p.Wards)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__Wards__DISTRICT___09A971A2");
            });
            OnModelCreatingPartial(builder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
