using System.ComponentModel.DataAnnotations;

namespace WebProje2023.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Kullanıcı Adı Giriniz")]
        [StringLength(30)]
        public string kullaniciEmail { get; set; }

        [Required(ErrorMessage = "Şifre Giriniz")]
        [MinLength(6, ErrorMessage = "Şifre uzunluğu 6 karakterden uzun olmalı")]
        [MaxLength(20, ErrorMessage = "Şifre uzunluğu 20 karakterden uzun olmamalı")]
        public string KullaniciSifre { get; set; }
    }
}
