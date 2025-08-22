using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimsProcessing.Models
{
	public class Claim
	{
		[Key]
		public int ClaimId { get; set; }

		[Required]
		[StringLength(50)]
		public string PolicyNumber { get; set; } = string.Empty;

		[Required]
		[StringLength(50)]
		public string ClaimType { get; set; } = string.Empty;

		[Required]
		[Column(TypeName = "text")]
		public string Description { get; set; } = string.Empty;

		[Column(TypeName = "decimal(10,2)")]
		[Range(0, 99999999.99)]
		public decimal ClaimAmount { get; set; }

		[Required]
		[StringLength(20)]
		public string Status { get; set; } = "In Review";   

		[Required]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		[Required]
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		[Required]
		public int AgentId { get; set; }
	}
}

