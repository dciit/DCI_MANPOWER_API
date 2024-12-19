using System;
using System.Collections.Generic;
using DCI_MANPOWER_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DCI_MANPOWER_API.Contexts;

public partial class DBSCM : DbContext
{
    public DBSCM()
    {
    }

    public DBSCM(DbContextOptions<DBSCM> options)
        : base(options)
    {
    }

    public virtual DbSet<MpckCheckInLog> MpckCheckInLogs { get; set; }

    public virtual DbSet<MpckDictionary> MpckDictionaries { get; set; }

    public virtual DbSet<MpckLayout> MpckLayouts { get; set; }

    public virtual DbSet<MpckObject> MpckObjects { get; set; }

    public virtual DbSet<MpckObjectMaster> MpckObjectMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.226.86;Database=dbSCM;TrustServerCertificate=True;uid=sa;password=decjapan");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Thai_CI_AS");

        modelBuilder.Entity<MpckCheckInLog>(entity =>
        {
            entity.HasKey(e => e.Nbr);

            entity.ToTable("MPCK_CheckInLog");

            entity.HasIndex(e => new { e.ObjCode, e.Ckdate, e.Cktype }, "IX_MPCK_CheckInLog").IsDescending(false, true, false);

            entity.HasIndex(e => new { e.ObjCode, e.CkdateTime, e.Cktype }, "IX_MPCK_CheckInLog_1").IsDescending(false, true, false);

            entity.Property(e => e.Nbr).HasMaxLength(50);
            entity.Property(e => e.Ckdate)
                .HasMaxLength(8)
                .HasColumnName("CKDate");
            entity.Property(e => e.CkdateTime)
                .HasColumnType("datetime")
                .HasColumnName("CKDateTime");
            entity.Property(e => e.Ckshift)
                .HasMaxLength(5)
                .HasColumnName("CKShift");
            entity.Property(e => e.Cktype)
                .HasMaxLength(10)
                .HasColumnName("CKType");
            entity.Property(e => e.EmpCode).HasMaxLength(5);
            entity.Property(e => e.ObjCode)
                .HasMaxLength(20)
                .HasColumnName("Obj_Code");
        });

        modelBuilder.Entity<MpckDictionary>(entity =>
        {
            entity.HasKey(e => new { e.DictType, e.DictCode, e.DictRefCode });

            entity.ToTable("MPCK_Dictionary");

            entity.HasIndex(e => e.DictType, "IX_MPCK_Dictionary");

            entity.HasIndex(e => new { e.DictType, e.DictRefCode }, "IX_MPCK_Dictionary_1");

            entity.Property(e => e.DictType)
                .HasMaxLength(50)
                .HasColumnName("Dict_Type");
            entity.Property(e => e.DictCode)
                .HasMaxLength(50)
                .HasColumnName("Dict_Code");
            entity.Property(e => e.DictRefCode)
                .HasMaxLength(50)
                .HasColumnName("Dict_RefCode");
            entity.Property(e => e.DictName)
                .HasMaxLength(250)
                .HasColumnName("Dict_Name");
            entity.Property(e => e.DictRefCode2)
                .HasMaxLength(50)
                .HasColumnName("Dict_RefCode2");
            entity.Property(e => e.DictRefName)
                .HasMaxLength(250)
                .HasColumnName("Dict_RefName");
            entity.Property(e => e.DictRefSubName)
                .HasMaxLength(250)
                .HasColumnName("Dict_RefSubName");
            entity.Property(e => e.DictSubName)
                .HasMaxLength(250)
                .HasColumnName("Dict_SubName");
        });

        modelBuilder.Entity<MpckLayout>(entity =>
        {
            entity.HasKey(e => e.LayoutCode);

            entity.ToTable("MPCK_Layout");

            entity.HasIndex(e => new { e.Factory, e.Line, e.SubLine }, "IX_MPCK_Layout");

            entity.Property(e => e.LayoutCode).HasMaxLength(20);
            entity.Property(e => e.BoardId).HasMaxLength(200);
            entity.Property(e => e.BypassMq)
                .HasMaxLength(10)
                .HasColumnName("BypassMQ");
            entity.Property(e => e.BypassSa)
                .HasMaxLength(10)
                .HasColumnName("BypassSA");
            entity.Property(e => e.Factory).HasMaxLength(10);
            entity.Property(e => e.LayoutName).HasMaxLength(50);
            entity.Property(e => e.LayoutStatus).HasMaxLength(10);
            entity.Property(e => e.LayoutSubName).HasMaxLength(100);
            entity.Property(e => e.Line).HasMaxLength(10);
            entity.Property(e => e.SubLine).HasMaxLength(20);
            entity.Property(e => e.UpdateBy).HasMaxLength(5);
            entity.Property(e => e.UpdateDate)
                .HasComment("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<MpckObject>(entity =>
        {
            entity.HasKey(e => e.ObjCode);

            entity.ToTable("MPCK_Object");

            entity.HasIndex(e => e.LayoutCode, "IX_MPCK_Object");

            entity.HasIndex(e => new { e.EmpCode, e.LayoutCode }, "IX_MPCK_Object_1");

            entity.HasIndex(e => e.EmpCode, "IX_MPCK_Object_2");

            entity.HasIndex(e => new { e.LayoutCode, e.ObjType }, "IX_MPCK_Object_3");

            entity.Property(e => e.ObjCode)
                .HasMaxLength(20)
                .HasColumnName("Obj_Code");
            entity.Property(e => e.EmpCode).HasMaxLength(5);
            entity.Property(e => e.LayoutCode).HasMaxLength(20);
            entity.Property(e => e.ObjBackgroundColor)
                .HasMaxLength(10)
                .HasColumnName("Obj_BackgroundColor");
            entity.Property(e => e.ObjBorderColor)
                .HasMaxLength(10)
                .HasColumnName("Obj_BorderColor");
            entity.Property(e => e.ObjBorderWidth)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Obj_BorderWidth");
            entity.Property(e => e.ObjFontColor)
                .HasMaxLength(10)
                .HasDefaultValueSql("(N'#000000')")
                .HasColumnName("Obj_FontColor");
            entity.Property(e => e.ObjFontSize)
                .HasDefaultValueSql("((14))")
                .HasColumnName("Obj_FontSize");
            entity.Property(e => e.ObjHeight)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Obj_Height");
            entity.Property(e => e.ObjInsertDt)
                .HasColumnType("datetime")
                .HasColumnName("Obj_InsertDT");
            entity.Property(e => e.ObjLastCheckDt)
                .HasColumnType("datetime")
                .HasColumnName("Obj_LastCheckDT");
            entity.Property(e => e.ObjMasterId)
                .HasMaxLength(20)
                .HasColumnName("Obj_MasterID");
            entity.Property(e => e.ObjPath)
                .HasColumnType("text")
                .HasColumnName("Obj_Path");
            entity.Property(e => e.ObjPicture)
                .HasMaxLength(200)
                .HasColumnName("Obj_Picture");
            entity.Property(e => e.ObjPosition)
                .HasMaxLength(2)
                .HasColumnName("Obj_Position");
            entity.Property(e => e.ObjPriority)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Obj_Priority");
            entity.Property(e => e.ObjStatus)
                .HasMaxLength(10)
                .HasColumnName("Obj_Status");
            entity.Property(e => e.ObjSubtitle)
                .HasMaxLength(200)
                .HasColumnName("Obj_Subtitle");
            entity.Property(e => e.ObjTitle)
                .HasMaxLength(100)
                .HasColumnName("Obj_Title");
            entity.Property(e => e.ObjType)
                .HasMaxLength(10)
                .HasColumnName("Obj_Type");
            entity.Property(e => e.ObjWidth)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Obj_Width");
            entity.Property(e => e.ObjX).HasColumnName("Obj_X");
            entity.Property(e => e.ObjY).HasColumnName("Obj_Y");
        });

        modelBuilder.Entity<MpckObjectMaster>(entity =>
        {
            entity.HasKey(e => e.ObjMasterId).HasName("PK_LNS_OBJECT_MASTER");

            entity.ToTable("MPCK_Object_Master");

            entity.Property(e => e.ObjMasterId)
                .HasMaxLength(20)
                .HasColumnName("Obj_MasterID");
            entity.Property(e => e.LayoutCode).HasMaxLength(50);
            entity.Property(e => e.MstName)
                .HasMaxLength(50)
                .HasColumnName("Mst_Name");
            entity.Property(e => e.MstOrder)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Mst_Order");
            entity.Property(e => e.MstStatus)
                .HasMaxLength(20)
                .HasColumnName("Mst_Status");
            entity.Property(e => e.ObjSvg)
                .HasColumnType("text")
                .HasColumnName("OBJ_SVG");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
