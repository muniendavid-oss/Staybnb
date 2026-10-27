using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace Staybnb.Models;
public class ApplicationUser : IdentityUser { [StringLength(100)] public string DisplayName {get;set;}=""; public DateTime CreatedAt {get;set;}=DateTime.UtcNow; }