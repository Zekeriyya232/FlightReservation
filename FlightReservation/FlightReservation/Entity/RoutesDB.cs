using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProje2023.Entity
{
	[Table("Routes")]
	public class RoutesDB
	{
		[Key]
		public int Id { get; set; }

		[Required, StringLength(100)]
		public string sehirKalkis { get; set; }

		[Required, StringLength(100)]
		public string sehirVaris { get; set; }

		[Required, StringLength(100)]
		public string havalimaniKalkis { get; set; }

		[Required, StringLength(100)]
		public string havalimaniVaris { get; set; }

		[Required]
		public int biletFiyat { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[Column(TypeName = "Date")]
		public DateTime tarihKalkis { get; set; }

		[Required]
		[StringLength(50)]
		public string ucakModel { get; set; }

		[Required]
		public TimeOnly kalkisSaat { get; set; }
		[Required]
		public TimeOnly varisSaat { get; set; }
	}
}
