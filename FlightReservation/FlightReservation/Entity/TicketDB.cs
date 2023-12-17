using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProje2023.Entity
{
	[Table("Ticket")]
	public class TicketDB
	{
		[Key]
		public int Id { get; set; }
		[Required, StringLength(100)]
		public string havalimaniKalkis { get; set; }
		[Required, StringLength(100)]
		public string havalimaniVaris { get; set; }
		[Required, StringLength(100)]
		public string sehirKalkis { get; set; }
		[Required, StringLength(100)]
		public string sehirVaris { get; set; }
		[Required, StringLength(100)]
		public string yolcuAdi { get; set; }
		[Required, StringLength(100)]
		public string yolcuSoyadi { get; set; }
		[Required, StringLength(50)]
		public string ucakModel { get; set; }
		public string koltukNo { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[Column(TypeName = "Date")]
		public DateTime tarihKalkis { get; set; }

		[Required]
		public TimeSpan saatKalkis { get; set; }
		[Required]
		public TimeSpan saatVaris { get; set; }

		[Required]
		public int kullaniciId { get; set; }

		[Required]
		public int biletFiyat { get; set; }
	}
}
