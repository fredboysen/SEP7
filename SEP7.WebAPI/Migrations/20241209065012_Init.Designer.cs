﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SEP7.Database.Data;

#nullable disable

namespace SEP7.WebAPI.Migrations
{
    [DbContext(typeof(ApplicationDB))]
    [Migration("20241209065012_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("SEP7.WebAPI.Models.MaterialData", b =>
                {
                    b.Property<int>("MaterialDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaterialId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MaterialType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductID")
                        .HasColumnType("TEXT");

                    b.Property<float>("TotalWeight")
                        .HasColumnType("REAL");

                    b.HasKey("MaterialDataId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("ProductID");

                    b.ToTable("MatData");
                });

            modelBuilder.Entity("SEP7.WebAPI.Models.MaterialsTotal", b =>
                {
                    b.Property<int>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("ADP_Fossil_MJ")
                        .HasColumnType("REAL");

                    b.Property<float>("ADP_Minerals_Metals")
                        .HasColumnType("REAL");

                    b.Property<float>("AP_Mol_H_Eq")
                        .HasColumnType("REAL");

                    b.Property<float>("EP_Freshwater_kg_P")
                        .HasColumnType("REAL");

                    b.Property<float>("EP_Marine_kg_N")
                        .HasColumnType("REAL");

                    b.Property<float>("EP_Terrestrial_Mol_N_Eq")
                        .HasColumnType("REAL");

                    b.Property<float>("ETP_FW_CTUe")
                        .HasColumnType("REAL");

                    b.Property<float>("E_Fi_CTUe")
                        .HasColumnType("REAL");

                    b.Property<float>("E_Fm_CTUe")
                        .HasColumnType("REAL");

                    b.Property<float>("E_Fo_CTUe")
                        .HasColumnType("REAL");

                    b.Property<float>("GWP_Biogenic_kg_CO2")
                        .HasColumnType("REAL");

                    b.Property<float>("GWP_Fossil_kg_CO2")
                        .HasColumnType("REAL");

                    b.Property<float>("GWP_LULUC_kg_CO2")
                        .HasColumnType("REAL");

                    b.Property<float>("GWP_Total_kg_CO2")
                        .HasColumnType("REAL");

                    b.Property<float>("HTTP_C_CTUh")
                        .HasColumnType("REAL");

                    b.Property<float>("HTTP_NC_CTUh")
                        .HasColumnType("REAL");

                    b.Property<float>("HT_CI_CTUh")
                        .HasColumnType("REAL");

                    b.Property<float>("HT_CM_CTUh")
                        .HasColumnType("REAL");

                    b.Property<float>("HT_CO_CTUh")
                        .HasColumnType("REAL");

                    b.Property<float>("HT_NCI_CTUh")
                        .HasColumnType("REAL");

                    b.Property<float>("HT_NCM_CTUh")
                        .HasColumnType("REAL");

                    b.Property<float>("HT_NCO_CTUh")
                        .HasColumnType("REAL");

                    b.Property<float>("IRP_kBq_U235")
                        .HasColumnType("REAL");

                    b.Property<float>("LU_Pt")
                        .HasColumnType("REAL");

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("ODP_kg_CFC11")
                        .HasColumnType("REAL");

                    b.Property<float>("PM_Disease_Inc")
                        .HasColumnType("REAL");

                    b.Property<float>("POCP_kg_NMVOC")
                        .HasColumnType("REAL");

                    b.Property<string>("ProductID")
                        .HasColumnType("TEXT");

                    b.Property<float>("WDP_m3_Depriv")
                        .HasColumnType("REAL");

                    b.HasKey("MaterialId");

                    b.HasIndex("ProductID")
                        .IsUnique();

                    b.ToTable("MaterialsTotals");
                });

            modelBuilder.Entity("SEP7.WebAPI.Models.Product", b =>
                {
                    b.Property<string>("ProductID")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProductID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SEP7.WebAPI.Models.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("email")
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("role")
                        .HasColumnType("TEXT");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("user_id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SEP7.WebAPI.Models.MaterialData", b =>
                {
                    b.HasOne("SEP7.WebAPI.Models.MaterialsTotal", "MaterialsTotal")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SEP7.WebAPI.Models.Product", "Product")
                        .WithMany("MaterialData")
                        .HasForeignKey("ProductID");

                    b.Navigation("MaterialsTotal");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SEP7.WebAPI.Models.MaterialsTotal", b =>
                {
                    b.HasOne("SEP7.WebAPI.Models.Product", "Product")
                        .WithOne("MaterialsTotal")
                        .HasForeignKey("SEP7.WebAPI.Models.MaterialsTotal", "ProductID");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SEP7.WebAPI.Models.Product", b =>
                {
                    b.Navigation("MaterialData");

                    b.Navigation("MaterialsTotal");
                });
#pragma warning restore 612, 618
        }
    }
}
