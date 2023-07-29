using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace First_Project.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aboutf> Aboutfs { get; set; }
        public virtual DbSet<BankAccInfof> BankAccInfofs { get; set; }
        public virtual DbSet<Bankf> Bankfs { get; set; }
        public virtual DbSet<Categoryf> Categoryfs { get; set; }
        public virtual DbSet<Contactf> Contactfs { get; set; }
        public virtual DbSet<Homef> Homefs { get; set; }
        public virtual DbSet<Orderf> Orderves { get; set; }
        public virtual DbSet<Paymantf> Paymantfs { get; set; }
        public virtual DbSet<ProductOrderf> ProductOrderves { get; set; }
        public virtual DbSet<Productf> Productfs { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Rolef> Rolefs { get; set; }
        public virtual DbSet<Tastimoniel> Tastimoniels { get; set; }
        public virtual DbSet<Useraccf> Useraccfs { get; set; }
        public virtual DbSet<Userloginf> Userloginfs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=JOR17_User44;PASSWORD=zamus123;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("JOR17_USER44")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Aboutf>(entity =>
            {
                entity.ToTable("ABOUTF");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Info1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("INFO1");

                entity.Property(e => e.Info2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("INFO2");

                entity.Property(e => e.Team)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TEAM");

                entity.Property(e => e.Text1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TEXT1");
            });

            modelBuilder.Entity<BankAccInfof>(entity =>
            {
                entity.ToTable("BANK_ACC_INFOF");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.AccId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ACC_ID");

                entity.Property(e => e.Balance)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.CardNumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARD_NUMBER");

                entity.Property(e => e.Cvv)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CVV");

                entity.Property(e => e.ExpireDate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPIRE_DATE");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.BankAccInfofs)
                    .HasForeignKey(d => d.AccId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ACC_FKK");
            });

            modelBuilder.Entity<Bankf>(entity =>
            {
                entity.ToTable("BANKF");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Balance)
                    .HasColumnType("NUMBER")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.CardNumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARD_NUMBER");

                entity.Property(e => e.Cvv)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CVV");

                entity.Property(e => e.ExpireDate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPIRE_DATE");
            });

           
            modelBuilder.Entity<Categoryf>(entity =>
            {
                entity.ToTable("CATEGORYF");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_NAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");
            });

            modelBuilder.Entity<Contactf>(entity =>
            {
                entity.ToTable("CONTACTF");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Location)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION");

                entity.Property(e => e.Phonenumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Text1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TEXT1");
            });

           

            modelBuilder.Entity<Homef>(entity =>
            {
                entity.ToTable("HOMEF");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Imagepath1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH_1");

                entity.Property(e => e.Phonenumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Text1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TEXT_1");

                entity.Property(e => e.Text2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TEXT_2");
            });

            modelBuilder.Entity<Orderf>(entity =>
            {
                entity.ToTable("ORDERF");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.AccId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ACC_ID");

                entity.Property(e => e.DateOrder)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_ORDER");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.Orderves)
                    .HasForeignKey(d => d.AccId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ACC_FKKKK");
            });

            modelBuilder.Entity<Paymantf>(entity =>
            {
                entity.ToTable("PAYMANTF");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Balance)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("BALANCE");

                entity.Property(e => e.PayDate)
                    .HasColumnType("DATE")
                    .HasColumnName("PAY_DATE");

                entity.Property(e => e.PaymantId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PAYMANT_ID");

                entity.HasOne(d => d.Paymant)
                    .WithMany(p => p.Paymantfs)
                    .HasForeignKey(d => d.PaymantId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PAYMANT_FKK");
            });

           


            modelBuilder.Entity<ProductOrderf>(entity =>
            {
                entity.ToTable("PRODUCT_ORDERF");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.OrderId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("QUANTITY");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductOrderves)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ORDER_FK");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOrderves)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PRODUCT_FKK");
            });

            modelBuilder.Entity<Productf>(entity =>
            {
                entity.ToTable("PRODUCTF");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Price)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRICE");

                entity.Property(e => e.ProductfName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTF_NAME");

                entity.Property(e => e.Sale)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SALE");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Productfs)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("CATEGORY_FK");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("REPORT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.OrderId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.UseraccId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERACC_ID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("CATEGORY_FKKK");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ORDER_FKKK");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PRODUCT_FKKK");

                entity.HasOne(d => d.Useracc)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UseraccId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("USERACC_FKKK");
            });

           
            modelBuilder.Entity<Rolef>(entity =>
            {
                entity.ToTable("ROLEF");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<Tastimoniel>(entity =>
            {
                entity.ToTable("TASTIMONIELS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.AccId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ACC_ID");

                entity.Property(e => e.Message)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Publishdate)
                    .HasColumnType("DATE")
                    .HasColumnName("PUBLISHDATE");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.Tastimoniels)
                    .HasForeignKey(d => d.AccId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ACC_FKKK");
            });

           

            modelBuilder.Entity<Useraccf>(entity =>
            {
                entity.ToTable("USERACCF");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Fname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Lname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");
            });

            modelBuilder.Entity<Userloginf>(entity =>
            {
                entity.ToTable("USERLOGINF");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.AccId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ACC_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLE_ID");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.Userloginfs)
                    .HasForeignKey(d => d.AccId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ACC_FK");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userloginfs)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ROLE_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
