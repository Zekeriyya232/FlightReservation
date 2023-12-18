using System.ComponentModel.DataAnnotations;

namespace WebProje2023.Models
{
	public class SelectRouteVM
	{
		[Required]
		public string havalimaniKalkis {  get; set; }
		[Required]
		public string havalimaniVaris {  get; set; }

		[Required]
        [DataType(DataType.Date)]
        public DateTime tarih {  get; set; }

	}
}
