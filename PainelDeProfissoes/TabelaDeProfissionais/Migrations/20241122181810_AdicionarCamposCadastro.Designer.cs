﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TabelaDeProfissionais.Context;

#nullable disable

namespace TabelaDeProfissionais.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241122181810_AdicionarCamposCadastro")]
    partial class AdicionarCamposCadastro
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("TabelaDeProfissionais.Models.Profissional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Especialidade")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NumeroDocumento")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoDocumento")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Profissionais");
                });
#pragma warning restore 612, 618
        }
    }
}
