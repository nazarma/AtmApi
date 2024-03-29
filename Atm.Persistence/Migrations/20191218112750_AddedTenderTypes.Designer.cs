﻿// <auto-generated />
using Atm.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Atm.Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20191218112750_AddedTenderTypes")]
    partial class AddedTenderTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("Atm.Domain.LegalTender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Nominal")
                        .HasColumnType("REAL");

                    b.Property<string>("TenderType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LegalTender");
                });
#pragma warning restore 612, 618
        }
    }
}
