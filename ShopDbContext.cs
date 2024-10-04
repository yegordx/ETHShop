using Microsoft.EntityFrameworkCore;
using ETHShop.Entities;
using ETHShop.Configurations;

namespace ETHShop;

public class ShopDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ShopDbContext(DbContextOptions<ShopDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ETHTransaction> ETHTransactions { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<ShippingAddress> ShippingAddresses { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<WishList> WishLists { get; set; }
    public DbSet<WishListItem> WishListItems { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString(nameof(ShopDbContext)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CartItemConfig());
        modelBuilder.ApplyConfiguration(new CategoryConfig());
        modelBuilder.ApplyConfiguration(new ETHTransactionConfig());
        modelBuilder.ApplyConfiguration(new OrderConfig());
        modelBuilder.ApplyConfiguration(new OrderItemConfig());
        modelBuilder.ApplyConfiguration(new PaymentConfig());
        modelBuilder.ApplyConfiguration(new ProductConfig());
        modelBuilder.ApplyConfiguration(new ReviewConfig());
        modelBuilder.ApplyConfiguration(new SellerConfig());
        modelBuilder.ApplyConfiguration(new ShippingAddressConfig());
        modelBuilder.ApplyConfiguration(new ShoppingCartConfig());
        modelBuilder.ApplyConfiguration(new WishListConfig());
        modelBuilder.ApplyConfiguration(new WishListItemConfig());
        modelBuilder.ApplyConfiguration(new NotificationConfig());
        base.OnModelCreating(modelBuilder);
    }
}
