﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AAEmu.DBEditor.data.aaemu.login;

public partial class LoginContext : DbContext
{
    public LoginContext(DbContextOptions<LoginContext> options) : base(options)
    {
        //
    }

    public virtual DbSet<GameServers> GameServers { get; set; }

    public virtual DbSet<Updates> Updates { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<GameServers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("game_servers", tb => tb.HasComment("Server list"))
                .HasCharSet("utf8")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Hidden)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("hidden");
            entity.Property(e => e.Host)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("host");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("name");
            entity.Property(e => e.Port)
                .HasColumnType("int(11)")
                .HasColumnName("port");
        });

        modelBuilder.Entity<Updates>(entity =>
        {
            entity.HasKey(e => e.ScriptName).HasName("PRIMARY");

            entity
                .ToTable("updates", tb => tb.HasComment("Table containing SQL update script information"))
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.ScriptName).HasColumnName("script_name");
            entity.Property(e => e.InstallDate)
                .HasColumnType("datetime")
                .HasColumnName("install_date");
            entity.Property(e => e.Installed)
                .HasColumnType("tinyint(4)")
                .HasColumnName("installed");
            entity.Property(e => e.LastError)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("last_error");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("users", tb => tb.HasComment("Account login information"))
                .HasCharSet("utf8")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.Username, "username");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.BanReason)
                .HasComment("Ban reason to report back")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("ban_reason");
            entity.Property(e => e.Banned)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("banned");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("email");
            entity.Property(e => e.LastIp)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("last_ip");
            entity.Property(e => e.LastLogin)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("last_login");
            entity.Property(e => e.Password)
                .HasComment("Hashed password of the user")
                .HasColumnType("text")
                .HasColumnName("password");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}