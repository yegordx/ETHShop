﻿// <auto-generated />
using System;
using ETHShop;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ETHShop.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20240920162117_AddUserIdToShoppingCart")]
    partial class AddUserIdToShoppingCart
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ETHShop.Entities.CartItem", b =>
                {
                    b.Property<Guid>("CartItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CartID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uuid");

                    b.HasKey("CartItemID");

                    b.HasIndex("CartID");

                    b.HasIndex("ProductID");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("ETHShop.Entities.Category", b =>
                {
                    b.Property<Guid>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ETHShop.Entities.ETHTransaction", b =>
                {
                    b.Property<Guid>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("AmountETH")
                        .HasColumnType("double precision");

                    b.Property<string>("FromAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OrderID")
                        .HasColumnType("uuid");

                    b.Property<string>("ToAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TransactionHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TransactionID");

                    b.HasIndex("OrderID")
                        .IsUnique();

                    b.ToTable("ETHTransactions");
                });

            modelBuilder.Entity("ETHShop.Entities.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("ETHShop.Entities.Order", b =>
                {
                    b.Property<Guid>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("SellerID")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("TotalPriceETH")
                        .HasColumnType("double precision");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid");

                    b.HasKey("OrderID");

                    b.HasIndex("SellerID");

                    b.HasIndex("UserID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ETHShop.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("OrderItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderID")
                        .HasColumnType("uuid");

                    b.Property<double>("PriceETH")
                        .HasColumnType("double precision");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("OrderItemID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("ETHShop.Entities.Payment", b =>
                {
                    b.Property<Guid>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("AmountETH")
                        .HasColumnType("double precision");

                    b.Property<Guid>("OrderID")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TransactionHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PaymentID");

                    b.HasIndex("OrderID")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("ETHShop.Entities.Product", b =>
                {
                    b.Property<Guid>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryID")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("PriceETH")
                        .HasColumnType("double precision");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SellerID")
                        .HasColumnType("uuid");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("integer");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("SellerID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ETHShop.Entities.Review", b =>
                {
                    b.Property<Guid>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uuid");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid");

                    b.HasKey("ReviewID");

                    b.HasIndex("ProductID");

                    b.HasIndex("UserID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("ETHShop.Entities.Seller", b =>
                {
                    b.Property<Guid>("SellerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StoreDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid");

                    b.HasKey("SellerID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("ETHShop.Entities.ShippingAddress", b =>
                {
                    b.Property<Guid>("AddressID")
                        .HasColumnType("uuid");

                    b.Property<string>("AddressLine")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid");

                    b.HasKey("AddressID");

                    b.ToTable("ShippingAddresses");
                });

            modelBuilder.Entity("ETHShop.Entities.ShoppingCart", b =>
                {
                    b.Property<Guid>("CartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("CartID");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("ETHShop.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RegisteredAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("SellerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ShoppingCartID")
                        .HasColumnType("uuid");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WalletAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ETHShop.Entities.WishList", b =>
                {
                    b.Property<Guid>("WishListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid");

                    b.HasKey("WishListID");

                    b.HasIndex("UserID");

                    b.ToTable("WishLists");
                });

            modelBuilder.Entity("ETHShop.Entities.WishListItem", b =>
                {
                    b.Property<Guid>("WishListItemID")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WishListID")
                        .HasColumnType("uuid");

                    b.HasKey("WishListItemID");

                    b.HasIndex("ProductID");

                    b.ToTable("WishListItems");
                });

            modelBuilder.Entity("ETHShop.Entities.CartItem", b =>
                {
                    b.HasOne("ETHShop.Entities.ShoppingCart", "ShoppingCart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ETHShop.Entities.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("ETHShop.Entities.ETHTransaction", b =>
                {
                    b.HasOne("ETHShop.Entities.Order", "Order")
                        .WithOne("EthereumTransaction")
                        .HasForeignKey("ETHShop.Entities.ETHTransaction", "OrderID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ETHShop.Entities.Notification", b =>
                {
                    b.HasOne("ETHShop.Entities.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ETHShop.Entities.Order", b =>
                {
                    b.HasOne("ETHShop.Entities.Seller", "Seller")
                        .WithMany("OrdersHistory")
                        .HasForeignKey("SellerID")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("ETHShop.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seller");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ETHShop.Entities.OrderItem", b =>
                {
                    b.HasOne("ETHShop.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ETHShop.Entities.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ETHShop.Entities.Payment", b =>
                {
                    b.HasOne("ETHShop.Entities.Order", "Order")
                        .WithOne("Payment")
                        .HasForeignKey("ETHShop.Entities.Payment", "OrderID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ETHShop.Entities.Product", b =>
                {
                    b.HasOne("ETHShop.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ETHShop.Entities.Seller", "Seller")
                        .WithMany("Products")
                        .HasForeignKey("SellerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("ETHShop.Entities.Review", b =>
                {
                    b.HasOne("ETHShop.Entities.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ETHShop.Entities.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ETHShop.Entities.Seller", b =>
                {
                    b.HasOne("ETHShop.Entities.User", "User")
                        .WithOne("Seller")
                        .HasForeignKey("ETHShop.Entities.Seller", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ETHShop.Entities.ShippingAddress", b =>
                {
                    b.HasOne("ETHShop.Entities.User", "User")
                        .WithMany("ShippingAddresses")
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ETHShop.Entities.ShoppingCart", b =>
                {
                    b.HasOne("ETHShop.Entities.User", "User")
                        .WithOne("ShoppingCart")
                        .HasForeignKey("ETHShop.Entities.ShoppingCart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ETHShop.Entities.WishList", b =>
                {
                    b.HasOne("ETHShop.Entities.User", "User")
                        .WithMany("WishLists")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ETHShop.Entities.WishListItem", b =>
                {
                    b.HasOne("ETHShop.Entities.Product", "Product")
                        .WithMany("WishListItems")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ETHShop.Entities.WishList", "WishList")
                        .WithMany("WishListItems")
                        .HasForeignKey("WishListItemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("WishList");
                });

            modelBuilder.Entity("ETHShop.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ETHShop.Entities.Order", b =>
                {
                    b.Navigation("EthereumTransaction")
                        .IsRequired();

                    b.Navigation("OrderItems");

                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("ETHShop.Entities.Product", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("OrderItems");

                    b.Navigation("Reviews");

                    b.Navigation("WishListItems");
                });

            modelBuilder.Entity("ETHShop.Entities.Seller", b =>
                {
                    b.Navigation("OrdersHistory");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("ETHShop.Entities.ShoppingCart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("ETHShop.Entities.User", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("Orders");

                    b.Navigation("Reviews");

                    b.Navigation("Seller")
                        .IsRequired();

                    b.Navigation("ShippingAddresses");

                    b.Navigation("ShoppingCart")
                        .IsRequired();

                    b.Navigation("WishLists");
                });

            modelBuilder.Entity("ETHShop.Entities.WishList", b =>
                {
                    b.Navigation("WishListItems");
                });
#pragma warning restore 612, 618
        }
    }
}
