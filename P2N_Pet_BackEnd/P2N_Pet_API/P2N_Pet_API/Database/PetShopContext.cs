using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using P2N_Pet_API.Database.PetShopModels;

#nullable disable

namespace P2N_Pet_API.Database
{
    public partial class PetShopContext : DbContext
    {
        public PetShopContext()
        {
        }

        public PetShopContext(DbContextOptions<PetShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Age> Ages { get; set; }
        public virtual DbSet<Breed> Breeds { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Cartitem> Cartitems { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Petdetail> Petdetails { get; set; }
        public virtual DbSet<Petimage> Petimages { get; set; }
        public virtual DbSet<Petimagefor> Petimagefors { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sex> Sexes { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Statusdetail> Statusdetails { get; set; }
        public virtual DbSet<Statusorder> Statusorders { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=p2n_pet;user=root;password=nhattaisin9999", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_unicode_ci");

            modelBuilder.Entity<Age>(entity =>
            {
                entity.ToTable("age");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Orderview).HasColumnName("orderview");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");
            });

            modelBuilder.Entity<Breed>(entity =>
            {
                entity.ToTable("breed");

                entity.HasIndex(e => e.Breedid, "breedid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Breedid).HasColumnName("breedid");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");

                entity.HasOne(d => d.BreedNavigation)
                    .WithMany(p => p.InverseBreedNavigation)
                    .HasForeignKey(d => d.Breedid)
                    .HasConstraintName("breed_ibfk_1");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.HasIndex(e => e.Userid, "userid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("cart_ibfk_1");
            });

            modelBuilder.Entity<Cartitem>(entity =>
            {
                entity.ToTable("cartitem");

                entity.HasIndex(e => e.Cartid, "cartid");

                entity.HasIndex(e => e.Orderid, "orderid");

                entity.HasIndex(e => e.Petdetailid, "petdetailid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cartid).HasColumnName("cartid");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Petdetailid).HasColumnName("petdetailid");

                entity.Property(e => e.Pricediscount).HasColumnName("pricediscount");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.Cartitems)
                    .HasForeignKey(d => d.Cartid)
                    .HasConstraintName("cartitem_ibfk_1");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Cartitems)
                    .HasForeignKey(d => d.Orderid)
                    .HasConstraintName("cartitem_ibfk_3");

                entity.HasOne(d => d.Petdetail)
                    .WithMany(p => p.Cartitems)
                    .HasForeignKey(d => d.Petdetailid)
                    .HasConstraintName("cartitem_ibfk_2");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("color");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("contact");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .HasColumnName("phone");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Subject)
                    .HasMaxLength(255)
                    .HasColumnName("subject")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.Userid, "userid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Birthday)
                    .HasColumnType("datetime")
                    .HasColumnName("birthday");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .HasColumnName("phone");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("customer_ibfk_1");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.HasIndex(e => e.Cartid, "cartid");

                entity.HasIndex(e => e.Customerid, "customerid");

                entity.HasIndex(e => e.Statusorderid, "statusorderid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cartid).HasColumnName("cartid");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Customerid).HasColumnName("customerid");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Statusorderid).HasColumnName("statusorderid");

                entity.Property(e => e.Totalmoney).HasColumnName("totalmoney");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Cartid)
                    .HasConstraintName("order_ibfk_1");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("order_ibfk_2");

                entity.HasOne(d => d.Statusorder)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Statusorderid)
                    .HasConstraintName("order_ibfk_3");
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("pet");

                entity.HasIndex(e => e.Breedid, "breedid");

                entity.HasIndex(e => e.Supplierid, "supplierid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Breedid).HasColumnName("breedid");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Supplierid).HasColumnName("supplierid");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");

                entity.HasOne(d => d.Breed)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.Breedid)
                    .HasConstraintName("pet_ibfk_1");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.Supplierid)
                    .HasConstraintName("pet_ibfk_2");
            });

            modelBuilder.Entity<Petdetail>(entity =>
            {
                entity.ToTable("petdetail");

                entity.HasIndex(e => e.Ageid, "ageid");

                entity.HasIndex(e => e.Colorid, "colorid");

                entity.HasIndex(e => e.Petid, "petid");

                entity.HasIndex(e => e.Sexid, "sexid");

                entity.HasIndex(e => e.Sizeid, "sizeid");

                entity.HasIndex(e => e.Statusdetailid, "statusdetailid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ageid).HasColumnName("ageid");

                entity.Property(e => e.Colorid).HasColumnName("colorid");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Petid).HasColumnName("petid");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Sexid).HasColumnName("sexid");

                entity.Property(e => e.Sizeid).HasColumnName("sizeid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Statusdetailid).HasColumnName("statusdetailid");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");

                entity.HasOne(d => d.Age)
                    .WithMany(p => p.Petdetails)
                    .HasForeignKey(d => d.Ageid)
                    .HasConstraintName("petdetail_ibfk_4");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Petdetails)
                    .HasForeignKey(d => d.Colorid)
                    .HasConstraintName("petdetail_ibfk_2");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.Petdetails)
                    .HasForeignKey(d => d.Petid)
                    .HasConstraintName("petdetail_ibfk_1");

                entity.HasOne(d => d.Sex)
                    .WithMany(p => p.Petdetails)
                    .HasForeignKey(d => d.Sexid)
                    .HasConstraintName("petdetail_ibfk_5");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Petdetails)
                    .HasForeignKey(d => d.Sizeid)
                    .HasConstraintName("petdetail_ibfk_3");

                entity.HasOne(d => d.Statusdetail)
                    .WithMany(p => p.Petdetails)
                    .HasForeignKey(d => d.Statusdetailid)
                    .HasConstraintName("petdetail_ibfk_6");
            });

            modelBuilder.Entity<Petimage>(entity =>
            {
                entity.ToTable("petimage");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");
            });

            modelBuilder.Entity<Petimagefor>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Petimageid, e.Petdetailid })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("petimagefor");

                entity.HasIndex(e => e.Petdetailid, "petdetailid");

                entity.HasIndex(e => e.Petimageid, "petimageid");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Petimageid).HasColumnName("petimageid");

                entity.Property(e => e.Petdetailid).HasColumnName("petdetailid");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");

                entity.HasOne(d => d.Petdetail)
                    .WithMany(p => p.Petimagefors)
                    .HasForeignKey(d => d.Petdetailid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("petimagefor_ibfk_2");

                entity.HasOne(d => d.Petimage)
                    .WithMany(p => p.Petimagefors)
                    .HasForeignKey(d => d.Petimageid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("petimagefor_ibfk_1");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.ToTable("promotion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Fromdate)
                    .HasColumnType("datetime")
                    .HasColumnName("fromdate");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Todate)
                    .HasColumnType("datetime")
                    .HasColumnName("todate");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Sex>(entity =>
            {
                entity.ToTable("sex");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("size");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Orderview).HasColumnName("orderview");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Statusdetail>(entity =>
            {
                entity.ToTable("statusdetail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Statusorder>(entity =>
            {
                entity.ToTable("statusorder");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("supplier");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .HasColumnName("phone");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Role, "role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .HasColumnName("avatar")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Createuser).HasColumnName("createuser");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .HasColumnName("phone");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Updatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedate");

                entity.Property(e => e.Updateuser).HasColumnName("updateuser");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Role)
                    .HasConstraintName("users_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
