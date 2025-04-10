﻿// <auto-generated />
using InsuranceWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace InsuranceWebApp.Migrations
{
    [DbContext(typeof(HospitalDbContext))]
    partial class HospitalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InsuranceProject.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("InsuranceProject.Data.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CITY_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityId"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CITY_NAME");

                    b.HasKey("CityId")
                        .HasName("PK__Cities__6E64DFEA4D3DD7A5");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("InsuranceProject.Data.District", b =>
                {
                    b.Property<int>("DistrictId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DISTRICT_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DistrictId"));

                    b.Property<int?>("CityId")
                        .HasColumnType("int")
                        .HasColumnName("CITY_ID");

                    b.Property<string>("DistrictName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("DISTRICT_NAME");

                    b.HasKey("DistrictId")
                        .HasName("PK__District__F24D1D90912228B5");

                    b.HasIndex("CityId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("InsuranceProject.Data.Hospital", b =>
                {
                    b.Property<int>("HospitalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("HOSPITAL_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HospitalId"));

                    b.Property<string>("BillingTime")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("BILLING_TIME");

                    b.Property<int?>("CityId")
                        .HasColumnType("int")
                        .HasColumnName("CITY_ID");

                    b.Property<bool?>("Dental")
                        .HasColumnType("bit")
                        .HasColumnName("DENTAL");

                    b.Property<int?>("DistrictId")
                        .HasColumnType("int")
                        .HasColumnName("DISTRICT_ID");

                    b.Property<string>("HospitalAddress")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("HOSPITAL_ADDRESS");

                    b.Property<string>("HospitalName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("HOSPITAL_NAME");

                    b.Property<bool?>("InPatient")
                        .HasColumnType("bit")
                        .HasColumnName("IN_PATIENT");

                    b.Property<string>("InsuranceAndDirectBilling")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("INSURANCE_AND_DIRECT_BILLING");

                    b.Property<bool?>("IsPublicHospital")
                        .HasColumnType("bit")
                        .HasColumnName("IS_PUBLIC_HOSPITAL");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("decimal(8, 6)")
                        .HasColumnName("LATITUDE");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("decimal(9, 6)")
                        .HasColumnName("LONGITUDE");

                    b.Property<string>("Note")
                        .HasMaxLength(550)
                        .HasColumnType("nvarchar(550)")
                        .HasColumnName("NOTE");

                    b.Property<bool?>("OutPatient")
                        .HasColumnType("bit")
                        .HasColumnName("OUT_PATIENT");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("PHONE_NUMBER");

                    b.Property<int?>("WardId")
                        .HasColumnType("int")
                        .HasColumnName("WARD_ID");

                    b.HasKey("HospitalId")
                        .HasName("PK__Hospital__AAE760882E76082B");

                    b.HasIndex("CityId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("WardId");

                    b.ToTable("Hospitals");
                });

            modelBuilder.Entity("InsuranceProject.Data.Ward", b =>
                {
                    b.Property<int>("WardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("WARD_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WardId"));

                    b.Property<int?>("DistrictId")
                        .HasColumnType("int")
                        .HasColumnName("DISTRICT_ID");

                    b.Property<string>("WardName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("WARD_NAME");

                    b.HasKey("WardId")
                        .HasName("PK__Wards__313533870E9B7A5A");

                    b.HasIndex("DistrictId");

                    b.ToTable("Wards");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "0505aa16-eef8-4f47-b741-1f55c6db11a1",
                            Name = "Admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
                            Id = "6fc66c9d-6279-41f8-b7d1-6d9a8760accc",
                            Name = "SuperAdmin",
                            NormalizedName = "SuperAdmin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("InsuranceProject.Data.District", b =>
                {
                    b.HasOne("InsuranceProject.Data.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK__Districts__CITY___02FC7413");

                    b.Navigation("City");
                });

            modelBuilder.Entity("InsuranceProject.Data.Hospital", b =>
                {
                    b.HasOne("InsuranceProject.Data.City", "City")
                        .WithMany("Hospitals")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK__Hospitals__CITY___06CD04F7");

                    b.HasOne("InsuranceProject.Data.District", "District")
                        .WithMany("Hospitals")
                        .HasForeignKey("DistrictId")
                        .HasConstraintName("FK__Hospitals__DISTR__07C12930");

                    b.HasOne("InsuranceProject.Data.Ward", "Ward")
                        .WithMany("Hospitals")
                        .HasForeignKey("WardId")
                        .HasConstraintName("FK__Hospitals__WARD___08B54D69");

                    b.Navigation("City");

                    b.Navigation("District");

                    b.Navigation("Ward");
                });

            modelBuilder.Entity("InsuranceProject.Data.Ward", b =>
                {
                    b.HasOne("InsuranceProject.Data.District", "District")
                        .WithMany("Wards")
                        .HasForeignKey("DistrictId")
                        .HasConstraintName("FK__Wards__DISTRICT___09A971A2");

                    b.Navigation("District");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("InsuranceProject.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("InsuranceProject.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InsuranceProject.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("InsuranceProject.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InsuranceProject.Data.City", b =>
                {
                    b.Navigation("Districts");

                    b.Navigation("Hospitals");
                });

            modelBuilder.Entity("InsuranceProject.Data.District", b =>
                {
                    b.Navigation("Hospitals");

                    b.Navigation("Wards");
                });

            modelBuilder.Entity("InsuranceProject.Data.Ward", b =>
                {
                    b.Navigation("Hospitals");
                });
#pragma warning restore 612, 618
        }
    }
}
