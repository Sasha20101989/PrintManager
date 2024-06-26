﻿using Microsoft.EntityFrameworkCore;
using PrintManager.Persistence.Configurations;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence;

public partial class PrintingManagementContext(DbContextOptions<PrintingManagementContext> options) : DbContext(options)
{
    public virtual DbSet<BranchEntity> Branches { get; set; }

    public virtual DbSet<ConnectionTypeEntity> ConnectionTypes { get; set; }

    public virtual DbSet<EmployeeEntity> Employees { get; set; }

    public virtual DbSet<InstallationEntity> Installations { get; set; }

    public virtual DbSet<JobEntity> Jobs { get; set; }

    public virtual DbSet<PrinterEntity> Printers { get; set; }

    public virtual DbSet<StatusEntity> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BranchConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new InstallationConfiguration());
        modelBuilder.ApplyConfiguration(new JobConfiguration());
        modelBuilder.ApplyConfiguration(new PrinterConfiguration());
        modelBuilder.ApplyConfiguration(new StatusConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
