﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StLukesMedicalApp.API.Data;

#nullable disable

namespace StLukesMedicalApp.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AdmissionDoctor", b =>
                {
                    b.Property<Guid>("AdmissionsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DoctorsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AdmissionsId", "DoctorsId");

                    b.HasIndex("DoctorsId");

                    b.ToTable("AdmissionDoctor");
                });

            modelBuilder.Entity("AdmissionNurse", b =>
                {
                    b.Property<Guid>("AdmissionsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NursesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AdmissionsId", "NursesId");

                    b.HasIndex("NursesId");

                    b.ToTable("AdmissionNurse");
                });

            modelBuilder.Entity("AdmissionPatient", b =>
                {
                    b.Property<Guid>("AdmissionsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PatientsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AdmissionsId", "PatientsId");

                    b.HasIndex("PatientsId");

                    b.ToTable("AdmissionPatient");
                });

            modelBuilder.Entity("AppointmentDoctor", b =>
                {
                    b.Property<Guid>("AppointmentsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DoctorsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AppointmentsId", "DoctorsId");

                    b.HasIndex("DoctorsId");

                    b.ToTable("AppointmentDoctor");
                });

            modelBuilder.Entity("AppointmentPatient", b =>
                {
                    b.Property<Guid>("AppointmentsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PatientsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AppointmentsId", "PatientsId");

                    b.HasIndex("PatientsId");

                    b.ToTable("AppointmentPatient");
                });

            modelBuilder.Entity("BillingPatient", b =>
                {
                    b.Property<Guid>("BillingsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PatientsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BillingsId", "PatientsId");

                    b.HasIndex("PatientsId");

                    b.ToTable("BillingPatient");
                });

            modelBuilder.Entity("DoctorPrescription", b =>
                {
                    b.Property<Guid>("DoctorsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PerscriptionsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DoctorsId", "PerscriptionsId");

                    b.HasIndex("PerscriptionsId");

                    b.ToTable("DoctorPrescription");
                });

            modelBuilder.Entity("PatientPrescription", b =>
                {
                    b.Property<Guid>("PatientsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PrescriptionsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PatientsId", "PrescriptionsId");

                    b.HasIndex("PrescriptionsId");

                    b.ToTable("PatientPrescription");
                });

            modelBuilder.Entity("StLukesMedicalApp.API.Models.Domain.Admission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvailabilityStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admissions");
                });

            modelBuilder.Entity("StLukesMedicalApp.API.Models.Domain.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Diagnosis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("StLukesMedicalApp.API.Models.Domain.Billing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBilling")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalAmount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Billings");
                });

            modelBuilder.Entity("StLukesMedicalApp.API.Models.Domain.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Schedule")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("StLukesMedicalApp.API.Models.Domain.Nurse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BadgeNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Qualifications")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Schedule")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Nurses");
                });

            modelBuilder.Entity("StLukesMedicalApp.API.Models.Domain.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Age")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diagnosis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("NurseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PatientType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NurseId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("StLukesMedicalApp.API.Models.Domain.Prescription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateIssued")
                        .HasColumnType("datetime2");

                    b.Property<string>("Dosage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MedicationList")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("AdmissionDoctor", b =>
                {
                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Admission", null)
                        .WithMany()
                        .HasForeignKey("AdmissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Doctor", null)
                        .WithMany()
                        .HasForeignKey("DoctorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AdmissionNurse", b =>
                {
                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Admission", null)
                        .WithMany()
                        .HasForeignKey("AdmissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Nurse", null)
                        .WithMany()
                        .HasForeignKey("NursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AdmissionPatient", b =>
                {
                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Admission", null)
                        .WithMany()
                        .HasForeignKey("AdmissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppointmentDoctor", b =>
                {
                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Appointment", null)
                        .WithMany()
                        .HasForeignKey("AppointmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Doctor", null)
                        .WithMany()
                        .HasForeignKey("DoctorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppointmentPatient", b =>
                {
                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Appointment", null)
                        .WithMany()
                        .HasForeignKey("AppointmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BillingPatient", b =>
                {
                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Billing", null)
                        .WithMany()
                        .HasForeignKey("BillingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DoctorPrescription", b =>
                {
                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Doctor", null)
                        .WithMany()
                        .HasForeignKey("DoctorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Prescription", null)
                        .WithMany()
                        .HasForeignKey("PerscriptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PatientPrescription", b =>
                {
                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Prescription", null)
                        .WithMany()
                        .HasForeignKey("PrescriptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StLukesMedicalApp.API.Models.Domain.Patient", b =>
                {
                    b.HasOne("StLukesMedicalApp.API.Models.Domain.Nurse", null)
                        .WithMany("Patients")
                        .HasForeignKey("NurseId");
                });

            modelBuilder.Entity("StLukesMedicalApp.API.Models.Domain.Nurse", b =>
                {
                    b.Navigation("Patients");
                });
#pragma warning restore 612, 618
        }
    }
}
