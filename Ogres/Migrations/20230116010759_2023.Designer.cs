﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ogres.Models;

#nullable disable

namespace Ogres.Migrations
{
    [DbContext(typeof(BuffetBDContext))]
    [Migration("20230116010759_2023")]
    partial class _2023
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ogres.Models.Plat", b =>
                {
                    b.Property<int>("PlatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlatId"));

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<int>("Taille")
                        .HasColumnType("int");

                    b.Property<int?>("TypePlatId")
                        .HasColumnType("int");

                    b.HasKey("PlatId");

                    b.HasIndex("TypePlatId");

                    b.ToTable("Plats");
                });

            modelBuilder.Entity("Ogres.Models.TypePlat", b =>
                {
                    b.Property<int>("TypePlatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypePlatId"));

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Probabilite")
                        .HasColumnType("int");

                    b.HasKey("TypePlatId");

                    b.ToTable("TypePlats");
                });

            modelBuilder.Entity("Ogres.Models.Plat", b =>
                {
                    b.HasOne("Ogres.Models.TypePlat", "TypePlat")
                        .WithMany()
                        .HasForeignKey("TypePlatId");

                    b.Navigation("TypePlat");
                });
#pragma warning restore 612, 618
        }
    }
}
