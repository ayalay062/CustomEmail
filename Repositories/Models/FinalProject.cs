using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Repositories.Models
{
    public partial class FinalProject : DbContext
    {
        public FinalProject()
        {
        }

        public FinalProject(DbContextOptions<FinalProject> options)
            : base(options)
        {
        }

        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<FieldsForTables> FieldsForTables { get; set; }
        public virtual DbSet<FieldTypes> FieldTypes { get; set; }
        public virtual DbSet<FilterFields> FilterFields { get; set; }
        public virtual DbSet<FilterTable> FilterTable { get; set; }
        public virtual DbSet<FilterTypes> FilterTypes { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<TablesForProject> TablesForProject { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\מסמכים שלי\\חני\\c#\\DB\\Final_Project_.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Companies>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.ContractStartDate).HasColumnType("date");

                entity.Property(e => e.Logo)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ServerName)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FieldsForTables>(entity =>
            {
                entity.HasKey(e => e.FieldCode);

                entity.Property(e => e.FieldName).HasMaxLength(30);

                entity.HasOne(d => d.FieldTypeNavigation)
                    .WithMany(p => p.FieldsForTables)
                    .HasForeignKey(d => d.FieldType)
                    .HasConstraintName("FK__FieldsFor__Field__17036CC0");

                entity.HasOne(d => d.TableCodeNavigation)
                    .WithMany(p => p.FieldsForTables)
                    .HasForeignKey(d => d.TableCode)
                    .HasConstraintName("FK__FieldsFor__Table__2B3F6F97");
            });

            modelBuilder.Entity<FieldTypes>(entity =>
            {
                entity.HasKey(e => e.FieldCode);

                entity.Property(e => e.FieldCode).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(100);
            });

            modelBuilder.Entity<FilterFields>(entity =>
            {
                entity.Property(e => e.DateValue).HasColumnType("date");

                entity.Property(e => e.StringValue).HasMaxLength(100);

                entity.HasOne(d => d.FieldCodeNavigation)
                    .WithMany(p => p.FilterFields)
                    .HasForeignKey(d => d.FieldCode)
                    .HasConstraintName("FK__FilterFie__Field__2BFE89A6");

                entity.HasOne(d => d.FilterCodeNavigation)
                    .WithMany(p => p.FilterFields)
                    .HasForeignKey(d => d.FilterCode)
                    .HasConstraintName("FK__FilterFie__Filte__2DE6D218");

                entity.HasOne(d => d.FilterTypesCodeNavigation)
                    .WithMany(p => p.FilterFields)
                    .HasForeignKey(d => d.FilterTypesCode)
                    .HasConstraintName("FK__FilterFie__Filte__2B0A656D");
            });

            modelBuilder.Entity<FilterTable>(entity =>
            {
                entity.HasOne(d => d.ProjectCodeNavigation)
                    .WithMany(p => p.FilterTable)
                    .HasForeignKey(d => d.ProjectCode)
                    .HasConstraintName("FK__FilterTab__Proje__3A4CA8FD");

                entity.HasOne(d => d.TableCodeNavigation)
                    .WithMany(p => p.FilterTable)
                    .HasForeignKey(d => d.TableCode)
                    .HasConstraintName("FK__FilterTab__Table__2CF2ADDF");
            });

            modelBuilder.Entity<FilterTypes>(entity =>
            {
                entity.HasKey(e => e.FilterId);

                entity.Property(e => e.FilterId).HasColumnName("FilterID");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.HasOne(d => d.FilterCodeNavigation)
                    .WithMany(p => p.FilterTypes)
                    .HasForeignKey(d => d.FilterCode)
                    .HasConstraintName("FK__FilterTyp__Filte__160F4887");
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.HasKey(e => e.ProjectCode);

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.FieldNameWithTheEmailAddress).HasMaxLength(30);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.ProjectName).HasMaxLength(100);

                entity.Property(e => e.TableNameWithTheEmailField).HasMaxLength(30);

                entity.HasOne(d => d.CompanyCodeNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CompanyCode)
                    .HasConstraintName("FK__Projects__Compan__25869641");
            });

            modelBuilder.Entity<TablesForProject>(entity =>
            {
                entity.HasKey(e => e.TableCode);

                entity.Property(e => e.TableName).HasMaxLength(30);

                entity.HasOne(d => d.ProjectCodeNavigation)
                    .WithMany(p => p.TablesForProject)
                    .HasForeignKey(d => d.ProjectCode)
                    .HasConstraintName("FK__TablesFor__Proje__47DBAE45");
            });
        }
    }
}
