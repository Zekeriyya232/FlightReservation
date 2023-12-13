using System.ComponentModel.DataAnnotations;

namespace WebProje2023.Models
{
	public class TicketVM
	{
		public int Id { get; set; }
		public string havalimaniKalkis { get; set; }
		public string havalimaniVaris { get; set; }
		public string sehirKalkis { get; set; }
		public string sehirVaris { get; set; }
		public string yolcuAdi { get; set; }
		public string yolcuSoyadi { get; set; }
		public string ucakModel { get; set; }
		public string koltukNo { get; set; }

		[Required]
		public int biletFiyat { get; set; }

		[Required]
		public DateTime tarihKalkis { get; set; }

		public TimeOnly saatKalkis { get; set; }
		public TimeOnly saatVaris { get; set; }

		public int kullaniciId { get; set; }

	}
}
