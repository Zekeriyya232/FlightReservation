using System.ComponentModel.DataAnnotations;

namespace FlightReservation.Models
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
