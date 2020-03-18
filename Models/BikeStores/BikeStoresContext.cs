using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Models.BikeStores
{
    public partial class BikeStoresContext : DbContext
    {
        public BikeStoresContext()
        {
        }

        public BikeStoresContext(DbContextOptions<BikeStoresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<Brands> Brands { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Commissions> Commissions { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Promotions> Promotions { get; set; }
        public virtual DbSet<Staffs> Staffs { get; set; }
        public virtual DbSet<Stocks> Stocks { get; set; }
        public virtual DbSet<Stores> Stores { get; set; }
        public virtual DbSet<Targets> Targets { get; set; }
        public virtual DbSet<Taxes> Taxes { get; set; }
        public virtual DbSet<VwNetsalesBrands> VwNetsalesBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Addresses>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK__addresse__CAA247C8028926FC");

                entity.ToTable("addresses", "sales");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip_code")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Brands>(entity =>
            {
                entity.HasKey(e => e.BrandId)
                    .HasName("PK__brands__5E5A8E27503A21C2");

                entity.ToTable("brands", "production");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasColumnName("brand_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__categori__D54EE9B4E3CDBFD5");

                entity.ToTable("categories", "production");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("category_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
            
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category", "sales");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("category_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
            
            modelBuilder.Entity<Commissions>(entity =>
            {
                entity.HasKey(e => e.StaffId)
                    .HasName("PK__commissi__1963DD9C6C1FA32F");

                entity.ToTable("commissions", "sales");

                entity.Property(e => e.StaffId)
                    .HasColumnName("staff_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BaseAmount)
                    .HasColumnName("base_amount")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Commission)
                    .HasColumnName("commission")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TargetId).HasColumnName("target_id");

                entity.HasOne(d => d.Staff)
                    .WithOne(p => p.Commissions)
                    .HasForeignKey<Commissions>(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__commissio__staff__6B24EA82");

                entity.HasOne(d => d.Target)
                    .WithMany(p => p.Commissions)
                    .HasForeignKey(d => d.TargetId)
                    .HasConstraintName("FK__commissio__targe__6A30C649");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__customer__CD65CB854738D720");

                entity.ToTable("customers", "sales");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasColumnName("street")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip_code")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ItemId })
                    .HasName("PK__order_it__837942D49EEF8F4B");

                entity.ToTable("order_items", "sales");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.ListPrice)
                    .HasColumnName("list_price")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__order_ite__order__3A81B327");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__order_ite__produ__3B75D760");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__orders__4659622985706DF9");

                entity.ToTable("orders", "sales");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("order_date")
                    .HasColumnType("date");

                entity.Property(e => e.OrderStatus).HasColumnName("order_status");

                entity.Property(e => e.RequiredDate)
                    .HasColumnName("required_date")
                    .HasColumnType("date");

                entity.Property(e => e.ShippedDate)
                    .HasColumnName("shipped_date")
                    .HasColumnType("date");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__orders__customer__34C8D9D1");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orders__staff_id__36B12243");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__orders__store_id__35BCFE0A");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__products__47027DF5B4568E98");

                entity.ToTable("products", "production");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ListPrice)
                    .HasColumnName("list_price")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ModelYear).HasColumnName("model_year");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK__products__brand___29572725");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__products__catego__286302EC");
            });
            
            modelBuilder.Entity<Promotions>(entity =>
            {
                entity.HasKey(e => e.PromotionId)
                    .HasName("PK__promotio__2CB9556B7EE44E3B");

                entity.ToTable("promotions", "sales");

                entity.Property(e => e.PromotionId).HasColumnName("promotion_id");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("numeric(3, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ExpiredDate)
                    .HasColumnName("expired_date")
                    .HasColumnType("date");

                entity.Property(e => e.PromotionName)
                    .IsRequired()
                    .HasColumnName("promotion_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Staffs>(entity =>
            {
                entity.HasKey(e => e.StaffId)
                    .HasName("PK__staffs__1963DD9C5967A2F9");

                entity.ToTable("staffs", "sales");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__staffs__AB6E61642CCBD5A6")
                    .IsUnique();

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerId).HasColumnName("manager_id");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__staffs__manager___31EC6D26");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Staffs)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__staffs__store_id__30F848ED");
            });

            modelBuilder.Entity<Stocks>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.ProductId })
                    .HasName("PK__stocks__E68284D3C7839651");

                entity.ToTable("stocks", "production");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__stocks__product___3F466844");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__stocks__store_id__3E52440B");
            });

            modelBuilder.Entity<Stores>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("PK__stores__A2F2A30C510AD471");

                entity.ToTable("stores", "sales");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasColumnName("store_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasColumnName("street")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip_code")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });
            
            modelBuilder.Entity<Targets>(entity =>
            {
                entity.HasKey(e => e.TargetId)
                    .HasName("PK__targets__57D3816E25CC8974");

                entity.ToTable("targets", "sales");

                entity.Property(e => e.TargetId)
                    .HasColumnName("target_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Percentage)
                    .HasColumnName("percentage")
                    .HasColumnType("decimal(4, 2)");
            });
            
            modelBuilder.Entity<Taxes>(entity =>
            {
                entity.HasKey(e => e.TaxId)
                    .HasName("PK__taxes__129B86708AB9814C");

                entity.ToTable("taxes", "sales");

                entity.HasIndex(e => e.State)
                    .HasName("UQ__taxes__A9360BC310DD246B")
                    .IsUnique();

                entity.Property(e => e.TaxId).HasColumnName("tax_id");

                entity.Property(e => e.AvgLocalTaxRate)
                    .HasColumnName("avg_local_tax_rate")
                    .HasColumnType("decimal(3, 2)");

                entity.Property(e => e.CombinedRate)
                    .HasColumnName("combined_rate")
                    .HasColumnType("decimal(4, 2)")
                    .HasComputedColumnSql("([state_tax_rate]+[avg_local_tax_rate])");

                entity.Property(e => e.MaxLocalTaxRate)
                    .HasColumnName("max_local_tax_rate")
                    .HasColumnType("decimal(3, 2)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StateTaxRate)
                    .HasColumnName("state_tax_rate")
                    .HasColumnType("decimal(3, 2)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });
            
            modelBuilder.Entity<VwNetsalesBrands>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_netsales_brands", "sales");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasColumnName("brand_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.NetSales)
                    .HasColumnName("net_sales")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
