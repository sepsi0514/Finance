﻿// <auto-generated />
using System;
using DAO.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAO.Migrations
{
    [DbContext(typeof(FinanceDatasContext))]
    [Migration("20240501134437_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("DAO.DBModels.Category", b =>
                {
                    b.Property<int>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("UID");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("Uid");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DAO.DBModels.Person", b =>
                {
                    b.Property<int>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("UID");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Uid");

                    b.ToTable("People");
                });

            modelBuilder.Entity("DAO.DBModels.State", b =>
                {
                    b.Property<int>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("UID");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("Uid");

                    b.ToTable("States");
                });

            modelBuilder.Entity("DAO.DBModels.Transaction", b =>
                {
                    b.Property<int>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("UID");

                    b.Property<double?>("Amount")
                        .HasColumnType("REAL");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("CategoryID");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PersonId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("PersonID");

                    b.Property<int?>("StateId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("StateID");

                    b.Property<double?>("Tscreation")
                        .HasColumnType("REAL")
                        .HasColumnName("TSCreation");

                    b.Property<double?>("Tstransaction")
                        .HasColumnType("REAL")
                        .HasColumnName("TSTransaction");

                    b.Property<int?>("WalletId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("WalletID");

                    b.HasKey("Uid");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("DAO.DBModels.Wallet", b =>
                {
                    b.Property<int>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("UID");

                    b.Property<double?>("Balance")
                        .HasColumnType("REAL");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<int?>("IsCash")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Uid");

                    b.ToTable("Wallets");
                });
#pragma warning restore 612, 618
        }
    }
}
