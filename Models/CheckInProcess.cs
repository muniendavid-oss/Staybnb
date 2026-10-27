using System.ComponentModel.DataAnnotations;
namespace Staybnb.Models;
public class CheckInProcess { public int Id {get;set;} public int PropertyId {get;set;} public Property? Property {get;set;} [Required,StringLength(3000)] public string Instructions {get;set;}=""; [StringLength(1000)] public string? RequiredDocuments {get;set;} public DateTime UpdatedAt {get;set;}=DateTime.UtcNow; }