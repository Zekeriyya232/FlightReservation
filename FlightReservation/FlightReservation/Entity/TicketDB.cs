﻿using System.ComponentModel.DataAnnotations;
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
		public TimeOnly saatKalkis { get; set; }
		[Required]
		public TimeOnly saatVaris { get; set; }

		[Required]
		public int kullaniciId { get; set; }
	}
}