﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingAPI.REPO;

#nullable disable

namespace ShoppingAPI.REPO.Migrations
{
    [DbContext(typeof(ShoppingContext))]
    [Migration("20221115062041_fixRating")]
    partial class fixRating
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ShoppingAPI.Data.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductVarationId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductVarationId");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CompletionTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeliveryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("PaymentTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(12)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("invoices");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.InvoicesDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<int>("Numbers")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("ProductVariationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductVariationId");

                    b.ToTable("InvoicesDetails");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("SKUS")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductVariationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductVariationId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.ProductRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductRatingId")
                        .HasColumnType("int");

                    b.Property<int>("ProductRatingImage")
                        .HasColumnType("int");

                    b.Property<int?>("ProductVariationId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("isEdit")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductRatingId");

                    b.HasIndex("ProductVariationId");

                    b.HasIndex("UserId");

                    b.ToTable("ProductRatings");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.ProductRatingImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<int>("ProductRatingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductRatingId");

                    b.ToTable("ProductRatingImages");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.ProductVariation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<double>("PriceCurrent")
                        .HasColumnType("float");

                    b.Property<double>("PriceOld")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("VariationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("VariationId");

                    b.ToTable("ProductVariations");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Expired")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAdress")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<string>("Refresh")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<string>("TokenId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2022, 11, 15, 13, 20, 41, 269, DateTimeKind.Local).AddTicks(9241),
                            IsTrash = false,
                            Name = "SuperAdmin"
                        });
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.ShoppingDeliveryAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(12)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("InfomationUsers");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FristName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("IdentityCard")
                        .HasColumnType("varchar(18)");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("Sex")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2022, 11, 15, 13, 20, 41, 270, DateTimeKind.Local).AddTicks(287),
                            IsTrash = false,
                            LastName = "Admin",
                            PasswordHash = "21232f297a57a5a743894a0e4a801fc3",
                            Sex = false,
                            Username = "Admin"
                        });
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2022, 11, 15, 13, 20, 41, 270, DateTimeKind.Local).AddTicks(363),
                            IsTrash = false,
                            RoleId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.Cart", b =>
                {
                    b.HasOne("ShoppingAPI.Data.Models.Product", "Product")
                        .WithMany("Carts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingAPI.Data.Models.ProductVariation", "ProductVariation")
                        .WithMany("Carts")
                        .HasForeignKey("ProductVarationId");

                    b.HasOne("ShoppingAPI.Data.Models.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ProductVariation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.Category", b =>
                {
                    b.HasOne("ShoppingAPI.Data.Models.Category", "CategoryParrent")
                        .WithMany("Categories")
                        .HasForeignKey("CategoryId");

                    b.Navigation("CategoryParrent");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.Invoice", b =>
                {
                    b.HasOne("ShoppingAPI.Data.Models.User", "User")
                        .WithMany("Invoices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.InvoicesDetails", b =>
                {
                    b.HasOne("ShoppingAPI.Data.Models.Invoice", "Invoice")
                        .WithMany("InvoicesDetails")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("ShoppingAPI.Data.Models.Product", "Product")
                        .WithMany("InvoicesDetails")
                        .HasForeignKey("ProductId");

                    b.HasOne("ShoppingAPI.Data.Models.ProductVariation", "ProductVariation")
                        .WithMany("InvoicesDetails")
                        .HasForeignKey("ProductVariationId");

                    b.Navigation("Invoice");

                    b.Navigation("Product");

                    b.Navigation("ProductVariation");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.Product", b =>
                {
                    b.HasOne("ShoppingAPI.Data.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.ProductImage", b =>
                {
                    b.HasOne("ShoppingAPI.Data.Models.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingAPI.Data.Models.ProductVariation", "ProductVariation")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductVariationId");

                    b.Navigation("Product");

                    b.Navigation("ProductVariation");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.ProductRating", b =>
                {
                    b.HasOne("ShoppingAPI.Data.Models.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceId");

                    b.HasOne("ShoppingAPI.Data.Models.Product", "Product")
                        .WithMany("ProductRatings")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingAPI.Data.Models.ProductRating", "ProductRatingReply")
                        .WithMany()
                        .HasForeignKey("ProductRatingId");

                    b.HasOne("ShoppingAPI.Data.Models.ProductVariation", "ProductVariation")
                        .WithMany("ProductRatings")
                        .HasForeignKey("ProductVariationId");

                    b.HasOne("ShoppingAPI.Data.Models.User", "User")
                        .WithMany("ProductRatings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Product");

                    b.Navigation("ProductRatingReply");

                    b.Navigation("ProductVariation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.ProductRatingImage", b =>
                {
                    b.HasOne("ShoppingAPI.Data.Models.ProductRating", "ProductRating")
                        .WithMany("productRatingImages")
                        .HasForeignKey("ProductRatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductRating");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.ProductVariation", b =>
                {
                    b.HasOne("ShoppingAPI.Data.Models.Product", "Product")
                        .WithMany("ProductVariations")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingAPI.Data.Models.ProductVariation", "ProductVariate")
                        .WithMany("ProductVariations")
                        .HasForeignKey("VariationId");

                    b.Navigation("Product");

                    b.Navigation("ProductVariate");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.RefreshToken", b =>
                {
                    b.HasOne("ShoppingAPI.Data.Models.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.ShoppingDeliveryAddress", b =>
                {
                    b.HasOne("ShoppingAPI.Data.Models.User", "User")
                        .WithMany("ShoppingDeliveryAddresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.UserRole", b =>
                {
                    b.HasOne("ShoppingAPI.Data.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingAPI.Data.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.Category", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.Invoice", b =>
                {
                    b.Navigation("InvoicesDetails");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.Product", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("InvoicesDetails");

                    b.Navigation("ProductImages");

                    b.Navigation("ProductRatings");

                    b.Navigation("ProductVariations");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.ProductRating", b =>
                {
                    b.Navigation("productRatingImages");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.ProductVariation", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("InvoicesDetails");

                    b.Navigation("ProductImages");

                    b.Navigation("ProductRatings");

                    b.Navigation("ProductVariations");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("ShoppingAPI.Data.Models.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Invoices");

                    b.Navigation("ProductRatings");

                    b.Navigation("RefreshTokens");

                    b.Navigation("ShoppingDeliveryAddresses");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
