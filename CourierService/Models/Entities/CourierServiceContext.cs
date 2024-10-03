using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CourierService.Models.Entities;

public partial class CourierServiceContext : DbContext
{

    public virtual DbSet<CargoType> CargoTypes { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Courier> Couriers { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    public CourierServiceContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=0000;Database=CourierService; Include Error Detail=true");
    }
}