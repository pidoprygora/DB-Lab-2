using System;
using System.Collections.Generic;
using ManeskinDomain.Model;
using Microsoft.EntityFrameworkCore;

namespace ManeskinInfrastructure;

public partial class ManeskinWContext : DbContext
{
    public ManeskinWContext()
    {
    }

    public ManeskinWContext(DbContextOptions<ManeskinWContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Albumss> Albumsses { get; set; }

    public virtual DbSet<Clip> Clips { get; set; }

    public virtual DbSet<FanProject> FanProjects { get; set; }

    public virtual DbSet<Festival> Festivals { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Perfomance> Perfomances { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<Query> Query { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=polinap\\SQLEXPRESS03; Database=ManeskinW; Trusted_Connection=True; TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Albumss>(entity =>
        {
            entity.HasKey(e => e.AlbumId);

            entity.ToTable("Albumss");

            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.Tittle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tittle");
            entity.Property(e => e.YearRelease).HasColumnName("year_release");
        });

        modelBuilder.Entity<Clip>(entity =>
        {
            entity.HasKey(e => e.VideoId);

            entity.Property(e => e.VideoId).HasColumnName("video_id");
            entity.Property(e => e.DataRelease).HasColumnName("data_release");
            entity.Property(e => e.MadeBy)
                .HasColumnType("text")
                .HasColumnName("made_by");
            entity.Property(e => e.SongId).HasColumnName("song_id");

            entity.HasOne(d => d.Song).WithMany(p => p.Clips)
                .HasForeignKey(d => d.SongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clips_Song");
        });

        modelBuilder.Entity<FanProject>(entity =>
        {
            entity.HasKey(e => e.ProjectId);

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.PerfomanceId).HasColumnName("perfomance_id");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("project_name");

            entity.HasOne(d => d.Perfomance).WithMany(p => p.FanProjects)
                .HasForeignKey(d => d.PerfomanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FanProjects_Perfomances");
        });

        modelBuilder.Entity<Festival>(entity =>
        {
            entity.Property(e => e.FestivalId).HasColumnName("festival_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.FestivalName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("festival_name");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.PerfomanceId).HasColumnName("perfomance_id");

            entity.HasOne(d => d.Location).WithMany(p => p.Festivals)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Festivals_Locations");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Instrument)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("instrument");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");

        });

        modelBuilder.Entity<Perfomance>(entity =>
        {
            entity.Property(e => e.PerfomanceId).HasColumnName("perfomance_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.FestivalId).HasColumnName("festival_id");
            entity.Property(e => e.Information)
                .HasColumnType("text")
                .HasColumnName("information");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.TourId).HasColumnName("tour_id");

            entity.HasOne(d => d.Location).WithMany(p => p.Perfomances)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Perfomances_Locations");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.ToTable("Song");

            entity.Property(e => e.SongId).HasColumnName("song_id");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.DataRelease).HasColumnName("data_release");
            entity.Property(e => e.Duration)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("duration");
            entity.Property(e => e.Tittle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tittle");

            entity.HasOne(d => d.Album).WithMany(p => p.Songs)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Song_Albumss");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.Property(e => e.TourId).HasColumnName("tour_id");
            entity.Property(e => e.NameTour)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name_tour");
            entity.Property(e => e.TourBegin).HasColumnName("tour_begin");
            entity.Property(e => e.TourEnd).HasColumnName("tour_end");
        });


        modelBuilder.Entity<Query>(entity =>
        {
            entity.HasKey(e => e.Id); // Вказівка, що Id є первинним ключем
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
