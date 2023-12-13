using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebProje2023.Entity;

namespace FlightReservation.Entity
{
	[Table("Airport")]
	public class AirportDB
	{
		[Key]
		public int Id { get; set; }
		[Required,StringLength(100)]
		public string havalimaniAdi { get; set; }

		[Required, StringLength(100)]
		public string sehir { get; set; }
		[Required, StringLength(20)]
		public string havalimaniKisaltma { get; set; }
		[Required, StringLength(50)]
		public string ulke { get; set; }

	

	}
}
