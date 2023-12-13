using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProje2023.Entity
{
	[Table("Plane")]
	public class PlaneDB
	{
		[Key]
		public int Id { get; set; }

		[Required, StringLength(100)]
		public string ucakModel { get; set; }

		[Required]
		public int koltukSayisi { get; set; }
	}
}
