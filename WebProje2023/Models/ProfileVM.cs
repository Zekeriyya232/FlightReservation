using System.ComponentModel.DataAnnotations;

namespace WebProje2023.Models
{
	public class ProfileVM
	{
		public string kullaniciAdi { get; set; }

		public string kullaniciSoyadi { get; set; }

		public string kullaniciEmail { get; set; }
		public string Phone { get; set; }

		public DateTime kullaniciDogum { get; set; }

		public DateTime KayitTarih { get; set; }  //düzelt

		//biletler gelebilir belki
	}
}
