using System.ComponentModel.DataAnnotations;

namespace WebProje2023.Models
{
	public class PlaneVM
	{
		public int Id { get; set; }
		[Required, StringLength(100)]
		public string ucakModel { get; set; }
		public int koltukSayisi { get; set; }
	}
}
