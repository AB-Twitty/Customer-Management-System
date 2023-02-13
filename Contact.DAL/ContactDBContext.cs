using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Contact.DAL
{
    public partial class ContactDBContext : DbContext
    {
        public ContactDBContext()
        {
        }

        public ContactDBContext(DbContextOptions<ContactDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactType> ContactTypes { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerAttachment> CustomerAttachments { get; set; } = null!;
        public virtual DbSet<CustomerContact> CustomerContacts { get; set; } = null!;
        public virtual DbSet<Nationality> Nationalities { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-DPHL3S8;Database=ContactDB;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.Property(e => e.TypeName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ValidationExpression)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.NationalId, "UQ__Customer__E9AA32FAF3DE7185")
                    .IsUnique();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerImageUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CustomerImageURL");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NationalId)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CustomerCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customers_CreatorId_FK");

                entity.HasOne(d => d.Modifier)
                    .WithMany(p => p.CustomerModifiers)
                    .HasForeignKey(d => d.ModifierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customers_ModifierId_FK");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.NationalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customers_NationalityId_FK");
            });

            modelBuilder.Entity<CustomerAttachment>(entity =>
            {
                entity.Property(e => e.AttachmentUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("AttachmentURL");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CustomerAttachmentCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerAttachments_Creator_FK");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerAttachments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerAttachments_Customer_FK");

                entity.HasOne(d => d.Modifier)
                    .WithMany(p => p.CustomerAttachmentModifiers)
                    .HasForeignKey(d => d.ModifierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerAttachments_Modifier_FK");
            });

            modelBuilder.Entity<CustomerContact>(entity =>
            {
                entity.Property(e => e.Contact)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.ContactType)
                    .WithMany(p => p.CustomerContacts)
                    .HasForeignKey(d => d.ContactTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerContacts_ContactTypeId_FK");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CustomerContactCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customerContacts_Creator_FK");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerContacts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerContacts_CustomerId_FK");

                entity.HasOne(d => d.Modifier)
                    .WithMany(p => p.CustomerContactModifiers)
                    .HasForeignKey(d => d.ModifierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerContacts_Modifier_FK");
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.Property(e => e.NationalityName)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username, "UQ__Users__536C85E4F2CC22E3")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__A9D1053446A4E5FB")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
