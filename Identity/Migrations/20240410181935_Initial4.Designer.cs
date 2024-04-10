﻿// <auto-generated />
using System;
using INTEXII.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace INTEXII.Migrations.Product
{
    [DbContext(typeof(ProductContext))]
    [Migration("20240410181935_Initial4")]
    partial class Initial4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("INTEXII.Models.Customer", b =>
                {
                    b.Property<int>("customer_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("customer_ID"));

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<DateOnly>("birth_date")
                        .HasColumnType("date");

                    b.Property<string>("country_of_residence")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("customer_ID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("INTEXII.Models.LineItem", b =>
                {
                    b.Property<int>("transaction_ID")
                        .HasColumnType("int");

                    b.Property<int>("product_ID")
                        .HasColumnType("int");

                    b.Property<int>("qty")
                        .HasColumnType("int");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.HasKey("transaction_ID", "product_ID");

                    b.ToTable("LineItems");
                });

            modelBuilder.Entity("INTEXII.Models.Order", b =>
                {
                    b.Property<int>("transaction_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("transaction_ID"));

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.Property<string>("bank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country_of_transaction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("customer_ID")
                        .HasColumnType("int");

                    b.Property<DateOnly>("date")
                        .HasColumnType("date");

                    b.Property<string>("day_of_week")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("entry_mode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("fraud")
                        .HasColumnType("bit");

                    b.Property<string>("shipping_address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("time")
                        .HasColumnType("int");

                    b.Property<string>("type_of_card")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type_of_transaction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("transaction_ID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("INTEXII.Models.Product", b =>
                {
                    b.Property<int>("product_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("product_ID"));

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("img_link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("num_parts")
                        .HasColumnType("int");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<string>("primary_color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("secondary_color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("year")
                        .HasColumnType("int");

                    b.HasKey("product_ID");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}