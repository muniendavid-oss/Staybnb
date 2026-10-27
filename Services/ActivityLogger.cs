using Staybnb.Data; using Staybnb.Models;
namespace Staybnb.Services;
public class ActivityLogger(ApplicationDbContext db) { public async Task LogAsync(string? userId,string action,string details,string? entityType=null,string? entityId=null) { db.ActivityLogs.Add(new ActivityLog{UserId=userId,Action=action,Details=details,EntityType=entityType,EntityId=entityId}); await db.SaveChangesAsync(); } }