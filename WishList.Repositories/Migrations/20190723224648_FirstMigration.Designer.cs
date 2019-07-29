﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WishList.Repositories.Context;

namespace WishList.Repositories.Migrations
{
    [DbContext(typeof(WishListContext))]
    [Migration("20190723224648_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WishList.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WishList.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WishList.Domain.Entities.Wish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Wishes");
                });

            modelBuilder.Entity("WishList.Domain.Entities.Wish", b =>
                {
                    b.HasOne("WishList.Domain.Entities.Product", "Product")
                        .WithMany("Wishes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WishList.Domain.Entities.User", "User")
                        .WithMany("Wishes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
