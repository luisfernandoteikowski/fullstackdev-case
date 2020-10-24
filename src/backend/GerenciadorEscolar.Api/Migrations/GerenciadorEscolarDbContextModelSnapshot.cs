﻿// <auto-generated />
using System;
using GerenciadorEscolar.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GerenciadorEscolar.Api.Migrations
{
    [DbContext(typeof(GerenciadorEscolarDbContext))]
    partial class GerenciadorEscolarDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0-rc.2.20475.6");

            modelBuilder.Entity("GerenciadorEscolar.Entity.Escola", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("NumeroInep")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Escola");
                });

            modelBuilder.Entity("GerenciadorEscolar.Entity.Turma", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Ano")
                        .HasColumnType("integer");

                    b.Property<string>("Curso")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("EscolaId")
                        .HasColumnType("uuid");

                    b.Property<string>("Nome")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Serie")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Turma");
                });

            modelBuilder.Entity("GerenciadorEscolar.Entity.Turma", b =>
                {
                    b.HasOne("GerenciadorEscolar.Entity.Escola", "Escola")
                        .WithMany("Turmas")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Escola");
                });

            modelBuilder.Entity("GerenciadorEscolar.Entity.Escola", b =>
                {
                    b.Navigation("Turmas");
                });
#pragma warning restore 612, 618
        }
    }
}
