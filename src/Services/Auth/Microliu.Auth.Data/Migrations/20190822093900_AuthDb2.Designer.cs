﻿// <auto-generated />
using System;
using Microliu.Auth.DataMySQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Microliu.Auth.DataMySQL.Migrations
{
    [DbContext(typeof(AuthDbContext))]
    [Migration("20190822093900_AuthDb2")]
    partial class AuthDb2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("Microliu.Auth.Domain.Entities.Position", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateTime");

                    b.Property<int>("IsDelete");

                    b.Property<int>("IsEnabled");

                    b.Property<string>("Name");

                    b.Property<int>("Sort");

                    b.HasKey("Id");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("Microliu.Auth.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateTime");

                    b.Property<string>("Email")
                        .HasMaxLength(40);

                    b.Property<int>("IsDelete");

                    b.Property<int>("IsEnabled");

                    b.Property<string>("NickName")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("AuthUser");
                });

            modelBuilder.Entity("Microliu.Auth.Domain.Entities.UserPosition", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateTime");

                    b.Property<int>("IsDelete");

                    b.Property<int>("IsEnabled");

                    b.Property<string>("PositionId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserPosition");
                });

            modelBuilder.Entity("Microliu.Auth.Domain.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateTime");

                    b.Property<int>("IsDelete");

                    b.Property<int>("IsEnabled");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Microliu.Auth.Domain.Entities.UserPosition", b =>
                {
                    b.HasOne("Microliu.Auth.Domain.Entities.User", "User")
                        .WithMany("UserPositions")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
