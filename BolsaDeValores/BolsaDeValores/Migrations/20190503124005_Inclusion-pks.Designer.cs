﻿// <auto-generated />
using System;
using BolsaDeValores.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BolsaDeValores.Migrations
{
    [DbContext(typeof(BolsaDeValoresContext))]
    [Migration("20190503124005_Inclusion-pks")]
    partial class Inclusionpks
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BolsaDeValores.Models.Actions", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("categoryid");

                    b.Property<int?>("ownerid");

                    b.Property<int>("priceQuant");

                    b.Property<int>("quantMinSell");

                    b.Property<int>("quantity");

                    b.Property<bool>("status");

                    b.HasKey("id");

                    b.HasIndex("categoryid");

                    b.HasIndex("ownerid");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("BolsaDeValores.Models.Category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("description");

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("BolsaDeValores.Models.Client", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("email");

                    b.Property<string>("name");

                    b.Property<string>("password");

                    b.HasKey("id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("BolsaDeValores.Models.Actions", b =>
                {
                    b.HasOne("BolsaDeValores.Models.Category", "category")
                        .WithMany("Actions")
                        .HasForeignKey("categoryid");

                    b.HasOne("BolsaDeValores.Models.Client", "owner")
                        .WithMany("Actions")
                        .HasForeignKey("ownerid");
                });
#pragma warning restore 612, 618
        }
    }
}
