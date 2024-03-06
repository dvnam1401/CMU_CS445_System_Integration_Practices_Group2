﻿// <auto-generated />
using System;
using DashBoard.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DashBoard.API.Migrations
{
    [DbContext(typeof(ApplicationSQLDbContext))]
    [Migration("20240306200003_MyMigration")]
    partial class MyMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DashBoard.API.Models.Domain.BenefitPlan", b =>
                {
                    b.Property<decimal>("benefitId")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Benefit_Plan_ID");

                    b.Property<int>("PercentageCopay")
                        .HasColumnType("int")
                        .HasColumnName("Percentage_CoPay");

                    b.Property<decimal>("deductable")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Deductable");

                    b.Property<string>("planName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Plan_Name");

                    b.HasKey("benefitId");

                    b.ToTable("BenefitPlans");
                });

            modelBuilder.Entity("DashBoard.API.Models.Domain.EmergencyContact", b =>
                {
                    b.Property<decimal>("EmployeeID")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Employee_ID");

                    b.Property<string>("emergencyContactName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Emergency_Contact_Name");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Phone_Number");

                    b.Property<string>("relationship")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Relationship");

                    b.HasKey("EmployeeID");

                    b.ToTable("Emergency_Contacts");
                });

            modelBuilder.Entity("DashBoard.API.Models.Domain.Employee", b =>
                {
                    b.Property<int>("employeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idEmployee");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("employeeId"));

                    b.Property<int>("PayRatesId")
                        .HasColumnType("int");

                    b.Property<decimal>("SSN")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("SSN");

                    b.Property<int>("employeeNumber")
                        .HasColumnType("int")
                        .HasColumnName("Employee Number");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)")
                        .HasColumnName("First Name");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)")
                        .HasColumnName("Last Name");

                    b.Property<decimal>("paidLastYear")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Paid Last Year");

                    b.Property<decimal>("paidToDate")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Paid To Date");

                    b.Property<string>("payRate")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)")
                        .HasColumnName("Pay Rate");

                    b.Property<int>("payRatesIdPayRates")
                        .HasColumnType("int")
                        .HasColumnName("Pay Rates_idPay Rates");

                    b.Property<int>("vacationDays")
                        .HasColumnType("int")
                        .HasColumnName("Vacation Days");

                    b.HasKey("employeeId");

                    b.HasIndex("PayRatesId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("DashBoard.API.Models.Domain.Employment", b =>
                {
                    b.Property<decimal>("employeeId")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Employee_Id");

                    b.Property<string>("employmentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Employment_Status");

                    b.Property<DateTime?>("hireDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("Hire_Date");

                    b.Property<DateTime?>("lastReviewDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("Last_Review_Date");

                    b.Property<DateTime?>("rehireDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("Rehire_Date");

                    b.Property<DateTime?>("terminationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("Termination_Date");

                    b.Property<string>("workersCompCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Workers_Comp_Code");

                    b.HasKey("employeeId");

                    b.ToTable("Employment");
                });

            modelBuilder.Entity("DashBoard.API.Models.Domain.JobHistory", b =>
                {
                    b.Property<decimal>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("ID"));

                    b.Property<string>("department")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Department");

                    b.Property<decimal?>("departmentCode")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Departmen_Code");

                    b.Property<string>("division")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Division");

                    b.Property<decimal>("employeeID")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Employee_ID");

                    b.Property<DateTime?>("endDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("End_Date");

                    b.Property<bool?>("hazardousTraining")
                        .HasColumnType("bit")
                        .HasColumnName("Hazardous_Training");

                    b.Property<decimal?>("hoursPerWeek")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Hours_per_Week");

                    b.Property<string>("jobCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Job_Category");

                    b.Property<string>("jobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Job_Title");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Location");

                    b.Property<decimal?>("payPeriod")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Pay_Period");

                    b.Property<decimal?>("salaryType")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Salary_Type");

                    b.Property<DateTime?>("startDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("Start_Date");

                    b.Property<decimal?>("supervisor")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Supervisor");

                    b.HasKey("ID");

                    b.HasIndex("employeeID");

                    b.ToTable("Job_History");
                });

            modelBuilder.Entity("DashBoard.API.Models.Domain.PayRate", b =>
                {
                    b.Property<int>("PayRatesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idPay Rates");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PayRatesId"));

                    b.Property<decimal>("PTLevel")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("PT - Level C");

                    b.Property<string>("payRateName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)")
                        .HasColumnName("Pay Rate Name");

                    b.Property<int>("payType")
                        .HasColumnType("int")
                        .HasColumnName("Pay Type");

                    b.Property<decimal>("pyAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Pay Amount");

                    b.Property<decimal>("taxPercentage")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Tax Percentage");

                    b.Property<decimal>("value")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Value");

                    b.HasKey("PayRatesId");

                    b.ToTable("Pay Rates");
                });

            modelBuilder.Entity("DashBoard.API.Models.Domain.Personal", b =>
                {
                    b.Property<decimal>("employeeId")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Employee_ID");

                    b.Property<decimal>("BenefitPlanbenefitId")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("address1")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Address1");

                    b.Property<string>("address2")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Address2");

                    b.Property<decimal?>("benefitPlans")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Benefit_Plans");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("City");

                    b.Property<string>("driverLicense")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Drivers_License");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Email");

                    b.Property<string>("ethnicity")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Ethnicity");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("First_Name");

                    b.Property<bool?>("gender")
                        .HasColumnType("bit")
                        .HasColumnName("Gender");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Last_Name");

                    b.Property<string>("maritalStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Marital_Status");

                    b.Property<string>("middleInitial")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Middle_Initial");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Phone_Number");

                    b.Property<string>("securityNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Social_Security_Number");

                    b.Property<bool?>("shareholderStatus")
                        .HasColumnType("bit")
                        .HasColumnName("Shareholder_Status");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("State");

                    b.Property<decimal>("zipcode")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Zip");

                    b.HasKey("employeeId");

                    b.HasIndex("BenefitPlanbenefitId");

                    b.ToTable("Personal");
                });

            modelBuilder.Entity("DashBoard.API.Models.Domain.EmergencyContact", b =>
                {
                    b.HasOne("DashBoard.API.Models.Domain.Personal", "Personal")
                        .WithOne("EmergencyContact")
                        .HasForeignKey("DashBoard.API.Models.Domain.EmergencyContact", "EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personal");
                });

            modelBuilder.Entity("DashBoard.API.Models.Domain.Employee", b =>
                {
                    b.HasOne("DashBoard.API.Models.Domain.PayRate", "PayRates")
                        .WithMany("Employees")
                        .HasForeignKey("PayRatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PayRates");
                });

            modelBuilder.Entity("DashBoard.API.Models.Domain.Employment", b =>
                {
                    b.HasOne("DashBoard.API.Models.Domain.Personal", "Personal")
                        .WithOne("Employment")
                        .HasForeignKey("DashBoard.API.Models.Domain.Employment", "employeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personal");
                });

            modelBuilder.Entity("DashBoard.API.Models.Domain.JobHistory", b =>
                {
                    b.HasOne("DashBoard.API.Models.Domain.Personal", "Personal")
                        .WithMany("JobHistories")
                        .HasForeignKey("employeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personal");
                });

            modelBuilder.Entity("DashBoard.API.Models.Domain.Personal", b =>
                {
                    b.HasOne("DashBoard.API.Models.Domain.BenefitPlan", "BenefitPlan")
                        .WithMany("Personals")
                        .HasForeignKey("BenefitPlanbenefitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BenefitPlan");
                });

            modelBuilder.Entity("DashBoard.API.Models.Domain.BenefitPlan", b =>
                {
                    b.Navigation("Personals");
                });

            modelBuilder.Entity("DashBoard.API.Models.Domain.PayRate", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("DashBoard.API.Models.Domain.Personal", b =>
                {
                    b.Navigation("EmergencyContact")
                        .IsRequired();

                    b.Navigation("Employment")
                        .IsRequired();

                    b.Navigation("JobHistories");
                });
#pragma warning restore 612, 618
        }
    }
}