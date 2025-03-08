﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderService.Db;

#nullable disable

namespace OrderService.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20250308083854_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("OrderService.Entity.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateOrder")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdateOrder")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OrderService.Entity.OrderProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CartId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("OrderService.Entity.OrderProduct", b =>
                {
                    b.HasOne("OrderService.Entity.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
