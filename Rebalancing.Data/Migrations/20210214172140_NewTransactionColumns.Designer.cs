﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rebalancing.Data;

namespace Rebalancing.Data.Migrations
{
    [DbContext(typeof(RebalancingDbContext))]
    [Migration("20210214172140_NewTransactionColumns")]
    partial class NewTransactionColumns
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Rebalancing.Core.DesiredPosition", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("PercentOfAccount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("SecurityId")
                        .HasColumnType("int");

                    b.HasKey("PositionId");

                    b.HasIndex("SecurityId");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("Rebalancing.Core.Security", b =>
                {
                    b.Property<int>("SecurityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Symbol")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("SecurityId");

                    b.HasIndex("Symbol")
                        .IsUnique();

                    b.ToTable("Security");
                });

            modelBuilder.Entity("Rebalancing.Core.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("SecurityId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SettlementDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("TransactionId");

                    b.HasIndex("SecurityId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Rebalancing.Core.DesiredPosition", b =>
                {
                    b.HasOne("Rebalancing.Core.Security", "Security")
                        .WithMany()
                        .HasForeignKey("SecurityId");
                });

            modelBuilder.Entity("Rebalancing.Core.Transaction", b =>
                {
                    b.HasOne("Rebalancing.Core.Security", "Security")
                        .WithMany()
                        .HasForeignKey("SecurityId");
                });
#pragma warning restore 612, 618
        }
    }
}
