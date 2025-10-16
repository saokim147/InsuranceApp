using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InsuranceWebApp.Data;

public partial  class HospitalDbContext : IdentityDbContext<ApplicationUser>
{
    public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK_CITIES");

            entity.Property(e => e.CityId).HasColumnName("CITY_ID");
            entity.Property(e => e.CityName)
                .HasMaxLength(255)
                .HasColumnName("CITY_NAME");
            entity.Property(e => e.CityNameEn)
                .HasMaxLength(255)
                .HasColumnName("CITY_NAME_EN");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.DistrictId).HasName("PK_DISTRICTS");

            entity.HasIndex(e => e.CityId, "idx_districts_cities");

            entity.Property(e => e.DistrictId).HasColumnName("DISTRICT_ID");
            entity.Property(e => e.CityId).HasColumnName("CITY_ID");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(255)
                .HasColumnName("DISTRICT_NAME");
            entity.Property(e => e.DistrictNameEn)
                .HasMaxLength(255)
                .HasColumnName("DISTRICT_NAME_EN");

            entity.HasOne(d => d.City).WithMany(p => p.Districts)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_CITIES_DISTRICTS");
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(e => e.HospitalId).HasName("PK__Hospital__AAE76088313D7A41");

            entity.Property(e => e.HospitalId).HasColumnName("HOSPITAL_ID");
            entity.Property(e => e.BillingTime)
                .HasMaxLength(100)
                .HasColumnName("BILLING_TIME");
            entity.Property(e => e.BillingTimeEn)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BILLING_TIME_EN");
            entity.Property(e => e.CityId).HasColumnName("CITY_ID");
            entity.Property(e => e.Dental).HasColumnName("DENTAL");
            entity.Property(e => e.DistrictId).HasColumnName("DISTRICT_ID");
            entity.Property(e => e.HospitalAddress)
                .HasMaxLength(150)
                .HasColumnName("HOSPITAL_ADDRESS");
            entity.Property(e => e.HospitalAddressEn)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("HOSPITAL_ADDRESS_EN");
            entity.Property(e => e.HospitalName)
                .HasMaxLength(100)
                .HasColumnName("HOSPITAL_NAME");
            entity.Property(e => e.HospitalNameEn)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("HOSPITAL_NAME_EN");
            entity.Property(e => e.InPatient).HasColumnName("IN_PATIENT");
            entity.Property(e => e.InsuranceAndDirectBilling)
                .HasMaxLength(100)
                .HasColumnName("INSURANCE_AND_DIRECT_BILLING");
            entity.Property(e => e.InsuranceAndDirectBillingEn)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("INSURANCE_AND_DIRECT_BILLING_EN");
            entity.Property(e => e.IsBlackList).HasColumnName("IS_BLACK_LIST");
            entity.Property(e => e.IsPublicHospital).HasColumnName("IS_PUBLIC_HOSPITAL");
            entity.Property(e => e.Latitude).HasColumnName("LATITUDE");
            entity.Property(e => e.Longitude).HasColumnName("LONGITUDE");
            entity.Property(e => e.Note)
                .HasMaxLength(550)
                .HasColumnName("NOTE");
            entity.Property(e => e.NoteEn)
                .HasMaxLength(750)
                .HasColumnName("NOTE_EN");
            entity.Property(e => e.OutPatient).HasColumnName("OUT_PATIENT");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(100)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.WardId).HasColumnName("WARD_ID");

            entity.HasOne(d => d.City).WithMany(p => p.Hospitals)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_HOSPITALS_CITIES");

            entity.HasOne(d => d.District).WithMany(p => p.Hospitals)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_HOSPITALS_DISTRICTS");

            entity.HasOne(d => d.Ward).WithMany(p => p.Hospitals)
                .HasForeignKey(d => d.WardId)
                .HasConstraintName("FK_HOSPITALS_WARDS");
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.WardId).HasName("PK_WARDS");

            entity.HasIndex(e => e.DistrictId, "idx_wards_districts");

            entity.Property(e => e.WardId).HasColumnName("WARD_ID");
            entity.Property(e => e.DistrictId).HasColumnName("DISTRICT_ID");
            entity.Property(e => e.WardName)
                .HasMaxLength(255)
                .HasColumnName("WARD_NAME");
            entity.Property(e => e.WardNameEn)
                .HasMaxLength(255)
                .HasColumnName("WARD_NAME_EN");

            entity.HasOne(d => d.District).WithMany(p => p.Wards)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_WARDS_DISTRICTS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
