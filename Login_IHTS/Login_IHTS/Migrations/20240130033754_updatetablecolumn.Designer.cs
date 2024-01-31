﻿// <auto-generated />
using System;
using API_TimeTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API_TimeTracker.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240130033754_updatetablecolumn")]
    partial class updatetablecolumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API_TimeTracker.Models.LocationModel", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LOCATIONID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LocationId"));

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LOCATIONNAME");

                    b.HasKey("LocationId");

                    b.ToTable("LOCATIONS");
                });

            modelBuilder.Entity("API_TimeTracker.Models.ProjectModel", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PROJECTNAMEID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"));

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PROJECTNAME");

                    b.HasKey("ProjectId");

                    b.ToTable("PROJECTNAMES");
                });

            modelBuilder.Entity("API_TimeTracker.Models.TaskModel", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"));

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time")
                        .HasColumnName("ENDTIME");

                    b.Property<int>("LocationId")
                        .HasColumnType("int")
                        .HasColumnName("LOCATIONID");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("PROJECTNAMEID");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time")
                        .HasColumnName("STARTTIME");

                    b.Property<DateOnly>("TaskDate")
                        .HasColumnType("date")
                        .HasColumnName("CREATIONDATE");

                    b.Property<string>("TaskDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TASKDESCRIPTION");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("USERID");

                    b.Property<string>("UserStoryBugNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("USERSTORYORBUGNO");

                    b.HasKey("TaskId");

                    b.ToTable("TASKDETAILS");
                });

            modelBuilder.Entity("API_TimeTracker.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("USERID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("USERNAME");

                    b.Property<byte>("permission")
                        .HasColumnType("tinyint")
                        .HasColumnName("PERMISSION");

                    b.HasKey("UserId");

                    b.ToTable("USERDETAILS");
                });
#pragma warning restore 612, 618
        }
    }
}
