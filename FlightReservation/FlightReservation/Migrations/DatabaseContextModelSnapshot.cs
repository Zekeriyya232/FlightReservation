﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;
using WebProje2023.Entity;

#nullable disable

namespace WebProje2023.Migrations
{
	[DbContext(typeof(DatabaseContext))]
	partial class DatabaseContextModelSnapshot : ModelSnapshot
	{
		protected override void BuildModel(ModelBuilder modelBuilder)
		{
#pragma warning disable 612, 618
			modelBuilder
				.HasAnnotation("ProductVersion", "6.0.25")
				.HasAnnotation("Relational:MaxIdentifierLength", 63);

			NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

			modelBuilder.Entity("WebProje2023.Entity.KullaniciDB", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("integer");

					NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

					b.Property<DateTime>("KayitTarih")
						.HasColumnType("timestamp with time zone");

					b.Property<string>("KullaniciSoyadi")
						.IsRequired()
						.HasMaxLength(20)
						.HasColumnType("character varying(20)");

					b.Property<bool>("Locked")
						.HasColumnType("boolean");

					b.Property<string>("Phone")
						.IsRequired()
						.HasColumnType("text");

					b.Property<string>("Role")
						.IsRequired()
						.HasMaxLength(50)
						.HasColumnType("character varying(50)");

					b.Property<string>("kullaniciAdi")
						.IsRequired()
						.HasMaxLength(30)
						.HasColumnType("character varying(30)");

					b.Property<DateTime>("kullaniciDogum")
						.HasColumnType("Date");

					b.Property<string>("kullaniciEmail")
						.IsRequired()
						.HasMaxLength(50)
						.HasColumnType("character varying(50)");

					b.Property<string>("kullaniciSifre")
						.IsRequired()
						.HasMaxLength(100)
						.HasColumnType("character varying(100)");

					b.HasKey("Id");

					b.ToTable("Kullanici");
				});
#pragma warning restore 612, 618
		}
	}
}
