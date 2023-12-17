using System.ComponentModel.DataAnnotations;

namespace WebProje2023.Models
{
	public class SelectRouteVM
	{
		[Required]
		public string havalimaniKalkis {  get; set; }
		[Required]
		public string havalimaniVaris {  get; set; }
		public DateTime tarih {  get; set; }

	}
}
