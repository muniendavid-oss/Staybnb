using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Staybnb.Models;
namespace Staybnb.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {
 public DbSet<Property> Properties => Set<Property>(); public DbSet<PropertyImage> PropertyImages => Set<PropertyImage>();
 public DbSet<HostApplication> HostApplications => Set<HostApplication>(); public DbSet<Booking> Bookings => Set<Booking>();
 public DbSet<GuestDocument> GuestDocuments => Set<GuestDocument>(); public DbSet<CheckInProcess> CheckInProcesses => Set<CheckInProcess>();
 public DbSet<Message> Messages => Set<Message>(); public DbSet<Notification> Notifications => Set<Notification>(); public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
 protected override void OnModelCreating(ModelBuilder b) { base.OnModelCreating(b);
  b.Entity<Property>().Property(x=>x.PricePerNight).HasPrecision(18,2); b.Entity<Property>().Property(x=>x.CleaningFee).HasPrecision(18,2); b.Entity<Property>().Property(x=>x.ServiceFee).HasPrecision(18,2);
  b.Entity<Booking>().Property(x=>x.TotalPrice).HasPrecision(18,2);
  b.Entity<Property>().HasOne(x=>x.Host).WithMany().HasForeignKey(x=>x.HostId).OnDelete(DeleteBehavior.Restrict);
  b.Entity<Booking>().HasOne(x=>x.Guest).WithMany().HasForeignKey(x=>x.GuestId).OnDelete(DeleteBehavior.Restrict);
  b.Entity<Booking>().HasOne(x=>x.Property).WithMany(x=>x.Bookings).HasForeignKey(x=>x.PropertyId).OnDelete(DeleteBehavior.Restrict);
  b.Entity<Message>().HasOne(x=>x.Sender).WithMany().HasForeignKey(x=>x.SenderId).OnDelete(DeleteBehavior.Restrict);
  b.Entity<Message>().HasOne(x=>x.Recipient).WithMany().HasForeignKey(x=>x.RecipientId).OnDelete(DeleteBehavior.Restrict);
 }
}