﻿// <auto-generated />
using System;
using Microliu.EmailService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Microliu.EmailService.Data.Migrations
{
    [DbContext(typeof(EmailDbContext))]
    partial class EmailDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("Microliu.EmailService.Domain.Entities.EmailSend", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<string>("CopyTo")
                        .HasMaxLength(500);

                    b.Property<DateTimeOffset>("CreateTime");

                    b.Property<int>("Deleted");

                    b.Property<int>("Enabled");

                    b.Property<string>("From");

                    b.Property<string>("Subject");

                    b.Property<string>("To");

                    b.HasKey("Id");

                    b.ToTable("email_send");
                });
#pragma warning restore 612, 618
        }
    }
}
