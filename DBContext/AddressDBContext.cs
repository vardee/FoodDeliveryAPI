using backendTask.DataBase.Models;
using backendTask.DataBase;
using Microsoft.EntityFrameworkCore;
using backendTask.DBContext.Models;
using System;
using System.Collections.Generic;

namespace backendTask.DBContext
{
    public class AddressDBContext : DbContext
    {
        public AddressDBContext(DbContextOptions<AddressDBContext> options) : base(options) { }

        public DbSet<AsAddrObj> AsAddrObjs { get; set; }
        public DbSet<AsAdmHiearchy> AsAdmHiearchies { get; set; }
        public DbSet<AsHouses> AsHouses{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AddressDBContext;Username=postgres;Password=postgres");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AsAddrObj>(entity =>
            {
                entity.HasKey(e => e.id).HasName("PK_Addr_Objs");

                entity.ToTable("as_addr_obj", "fias");

                entity.Property(e => e.id)
                 .ValueGeneratedNever()
                .HasColumnName("id");
                entity.Property(e => e.objectid)
                .HasColumnName("objectid");
                entity.Property(e => e.objectguid)
                .HasColumnName("objectguid");
                entity.Property(e => e.changeid)
                .HasColumnName("changeid");
                entity.Property(e => e.name)
                .HasColumnName("name");
                entity.Property(e => e.typename)
                .HasColumnName("typename");
                entity.Property(e => e.level)
                .HasColumnName("level");
                entity.Property(e => e.opertypeid)
                .HasColumnName("opertypeid");
                entity.Property(e => e.previd)
                .HasColumnName("previd");
                entity.Property(e => e.nextid)
                .HasColumnName("nextid");
                entity.Property(e => e.updatedate)
                .HasColumnName("updatedate");
                entity.Property(e => e.enddate)
                .HasColumnName("enddate");
                entity.Property(e => e.startdate)
                .HasColumnName("startdate");
                entity.Property(e => e.isactual)
                .HasColumnName("isactual");
                entity.Property(e => e.isactive)
                .HasColumnName("isactive");
            });

            modelBuilder.Entity<AsAdmHiearchy>(entity =>
            {
                entity.HasKey(e => e.id).HasName("PK_Adm_Hiearchy");

                entity.ToTable("as_adm_hierarchy", "fias");

                entity.Property(e => e.id)
                .ValueGeneratedNever()
                .HasColumnName("id");
                entity.Property(e => e.objectid)
                .HasColumnName("objectid");
                entity.Property(e => e.parentobjid)
                .HasColumnName("parentobjid");
                entity.Property(e => e.changeid)
                .HasColumnName("changeid");
                entity.Property(e => e.regioncode)
                .HasColumnName("regioncode");
                entity.Property(e => e.areacode)
                .HasColumnName("areacode");
                entity.Property(e => e.citycode)
                .HasColumnName("citycode");
                entity.Property(e => e.placecode)
                .HasColumnName("placecode");
                entity.Property(e => e.plancode)
                .HasColumnName("plancode");
                entity.Property(e => e.streetcode)
                .HasColumnName("streetcode");
                entity.Property(e => e.previd)
                .HasColumnName("previd");
                entity.Property(e => e.nextid)
                .HasColumnName("nextid");
                entity.Property(e => e.updatedate)
                .HasColumnName("updatedate");
                entity.Property(e => e.startdate)
                .HasColumnName("startdate");
                entity.Property(e => e.enddate)
                .HasColumnName("enddate");
                entity.Property(e => e.isactive)
                .HasColumnName("isactive");
                entity.Property(e => e.path)
                .HasColumnName("path");
            });

            modelBuilder.Entity<AsHouses>(entity =>
            {
                entity.HasKey(e => e.id).HasName("PK_Houses");

                entity.ToTable("as_houses", "fias", tb => tb.HasComment("Сведения по номерам домов улиц городов и населенных пунктов"));

                entity.Property(e => e.id)
                .ValueGeneratedNever()
                .HasColumnName("id");
                entity.Property(e => e.objectid)
                .HasColumnName("objectid");
                entity.Property(e => e.objectguid)
                .HasColumnName("objectguid");
                entity.Property(e => e.changeid)
                .HasColumnName("changeid");
                entity.Property(e => e.housenum)
                .HasColumnName("housenum");
                entity.Property(e => e.addnum1)
                .HasColumnName("addnum1");
                entity.Property(e => e.addnum2)
                .HasColumnName("addnum2");
                entity.Property(e => e.housetype)
                .HasColumnName("housetype");
                entity.Property(e => e.addtype1)
                .HasColumnName("addtype1");
                entity.Property(e => e.addtype2)
                .HasColumnName("addtype2");
                entity.Property(e => e.opertypeid)
                .HasColumnName("opertypeid");
                entity.Property(e => e.previd)
                .HasColumnName("previd");
                entity.Property(e => e.nextid)
                .HasColumnName("nextid");
                entity.Property(e => e.updatedate)
                .HasColumnName("updatedate");
                entity.Property(e => e.startdate)
                .HasColumnName("startdate");
                entity.Property(e => e.enddate)
                .HasColumnName("enddate");
                entity.Property(e => e.isactual)
                .HasColumnName("isactual");
                entity.Property(e => e.isactive)
                .HasColumnName("isactive");
            });
        }
    }
}
