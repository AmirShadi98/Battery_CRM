﻿// <auto-generated />
using System;
using Battery_CRM.Infrastructure.Data.Sql.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Battery_CRM.Infrastructure.Data.Sql.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231231204125_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(700)
                        .HasColumnType("nvarchar(700)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Branches");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "تبریز",
                            CreateDate = new DateTime(2023, 12, 31, 20, 41, 24, 822, DateTimeKind.Utc).AddTicks(9578),
                            IsDelete = false,
                            Name = "امامیه",
                            PhoneNumber = 9146400136L
                        });
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.Factor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("FactorCode")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserId");

                    b.ToTable("Factors");
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSend")
                        .HasColumnType("bit");

                    b.Property<int>("ReciverId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "مدیر"
                        },
                        new
                        {
                            Id = 2,
                            Title = "کاربر عادی"
                        });
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateDate = new DateTime(2024, 1, 1, 0, 11, 24, 823, DateTimeKind.Local).AddTicks(4540),
                            Family = "شادی",
                            IsDelete = false,
                            Name = "امیر",
                            Password = "gdyb21LQTcIANtvYMT7QVQ==",
                            PhoneNumber = 0L,
                            RoleId = 1,
                            UserName = "amir"
                        });
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.UserBranch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("UserId");

                    b.ToTable("UserBranchs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BranchId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.Customer", b =>
                {
                    b.HasOne("Battery_CRM.Core.Domain.Models.Battery_CRM.Branch", "Branch")
                        .WithMany("Customers")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.Factor", b =>
                {
                    b.HasOne("Battery_CRM.Core.Domain.Models.Battery_CRM.Customer", "Customer")
                        .WithMany("Factors")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Battery_CRM.Core.Domain.Models.Battery_CRM.User", "User")
                        .WithMany("Factors")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.Message", b =>
                {
                    b.HasOne("Battery_CRM.Core.Domain.Models.Battery_CRM.Branch", "Branch")
                        .WithMany("Messages")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Battery_CRM.Core.Domain.Models.Battery_CRM.Customer", "Customer")
                        .WithMany("Messages")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Battery_CRM.Core.Domain.Models.Battery_CRM.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Customer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.User", b =>
                {
                    b.HasOne("Battery_CRM.Core.Domain.Models.Battery_CRM.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.UserBranch", b =>
                {
                    b.HasOne("Battery_CRM.Core.Domain.Models.Battery_CRM.Branch", "Branch")
                        .WithMany("UserBranches")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Battery_CRM.Core.Domain.Models.Battery_CRM.User", "User")
                        .WithMany("UserBranches")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.Branch", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Messages");

                    b.Navigation("UserBranches");
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.Customer", b =>
                {
                    b.Navigation("Factors");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Battery_CRM.Core.Domain.Models.Battery_CRM.User", b =>
                {
                    b.Navigation("Factors");

                    b.Navigation("Messages");

                    b.Navigation("UserBranches");
                });
#pragma warning restore 612, 618
        }
    }
}