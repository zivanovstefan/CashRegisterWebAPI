﻿// <auto-generated />
using CashRegister.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CashRegister.Infrastructure.Migrations
{
    [DbContext(typeof(CashRegisterDBContext))]
    [Migration("20220404124033_BillModelChanged")]
    partial class BillModelChanged
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CashRegister.Domain.Models.Bill", b =>
                {
                    b.Property<string>("BillNumber")
                        .HasColumnType("text");

                    b.Property<string>("CreditCardNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.HasKey("BillNumber");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("CashRegister.Domain.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CashRegister.Domain.Models.ProductBill", b =>
                {
                    b.Property<string>("BillNumber")
                        .HasColumnType("text");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("integer");

                    b.Property<decimal>("ProductsPrice")
                        .HasColumnType("numeric");

                    b.HasKey("BillNumber", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("BillProducts");
                });

            modelBuilder.Entity("CashRegister.Domain.Models.ProductBill", b =>
                {
                    b.HasOne("CashRegister.Domain.Models.Bill", "Bill")
                        .WithMany("BillProducts")
                        .HasForeignKey("BillNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CashRegister.Domain.Models.Product", "Product")
                        .WithMany("BillProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CashRegister.Domain.Models.Bill", b =>
                {
                    b.Navigation("BillProducts");
                });

            modelBuilder.Entity("CashRegister.Domain.Models.Product", b =>
                {
                    b.Navigation("BillProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
