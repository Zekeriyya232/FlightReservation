using System.ComponentModel.DataAnnotations;

namespace WebProje2023.Models
{
	public class RoutesVM
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string sehirKalkis { get; set; }

		[Required]
		[StringLength(100)]
		public string sehirVaris { get; set; }

		[Required]
		[StringLength(100)]
		public string havalimaniKalkis { get; set; }

		[Required]
		[StringLength(100)]
		public string havalimaniVaris { get; set; }

		[Required]
		public int biletFiyat { get; set; }
		[Required]
		public DateTime tarihKalkis { get; set; }
		[Required]
		[StringLength(50)]
		public string ucakModel { get; set; }

		[Required]
		public TimeSpan kalkisSaat { get; set; }
		[Required]
		public TimeSpan varisSaat { get; set; }

	}
}
