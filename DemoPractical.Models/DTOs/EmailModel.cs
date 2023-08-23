namespace DemoPractical.Models.DTOs
{
	public class EmailModel
	{
		public string To { get; set; } = null!;

		public string? UserName { get; set; }
		
		public string Subject { get; set; }

		public string Body { get; set; }

	}
}
