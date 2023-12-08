using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using WebProje2023.Entity;
using WebProje2023.Models;
using System.Net.Sockets;
using Microsoft.AspNetCore.Authorization;

namespace WebProje2023.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContex;
        private readonly IConfiguration _configuration;

        public AccountController(DatabaseContext databaseContex, IConfiguration configuration)
        {
            _databaseContex = databaseContex;
            _configuration = configuration;
        }
       
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            if(ModelState.IsValid)
            {
                string md5Crypto = _configuration.GetValue<string>("AppSettings:MD5Crypto");
                string cryptoPassword = loginVM.KullaniciSifre + md5Crypto;
                string hashedPassword = cryptoPassword.MD5();

                KullaniciDB Kullanici = _databaseContex.Kullanici.SingleOrDefault(x => x.kullaniciEmail == loginVM.kullaniciEmail && x.kullaniciSifre == hashedPassword);


                if (Kullanici != null)
                {
                    if (Kullanici.Locked)
                    {
                        ModelState.AddModelError(nameof(loginVM.kullaniciEmail), "Kullanıcı hesabı askıda.");
                        return View(loginVM);
                    }
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, Kullanici.Id.ToString()));
                    claims.Add(new Claim("kullaniciAdi", Kullanici.kullaniciAdi));
                    claims.Add(new Claim(ClaimTypes.Role, Kullanici.Role));
                    claims.Add(new Claim(ClaimTypes.Email, Kullanici.kullaniciEmail));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı ya da parola hatalı");
                };
            }

            return View(loginVM);
        }
		[AllowAnonymous]
		public IActionResult SignUp()
        {
            return View();
        }

		[AllowAnonymous]
		[HttpPost]
        public IActionResult SignUp(SignUpVm signUpVm)
        {
            if(ModelState.IsValid)
            {
                string md5Crypto = _configuration.GetValue<string>("AppSettings:MD5Crypto");
                string cryptoPassword = signUpVm.KullaniciSifre + md5Crypto;
                string hashedPassword = cryptoPassword.MD5();

                KullaniciDB kullanici = new()
                {
                    kullaniciAdi = signUpVm.kullaniciAdi,
                    KullaniciSoyadi = signUpVm.kullaniciSoyadi,
                    kullaniciSifre = hashedPassword,
                    kullaniciEmail = signUpVm.kullaniciEmail,
                    kullaniciDogum = signUpVm.kullaniciDogum,
                    Phone = signUpVm.Phone
                };
                
                _databaseContex.Kullanici.Add(kullanici);
                _databaseContex.SaveChanges();
                return View("Login");
            }
            else
            {
                return View(signUpVm);
            }
        }
        public IActionResult Profile()
        {
			string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
			int intId = Convert.ToInt32(id);

			ViewBag.kullaniciId = intId;

			KullaniciDB kullanici = _databaseContex.Kullanici.Find(intId);
			ProfileVM kullaniciProfil = new ProfileVM();
			kullaniciProfil.kullaniciAdi = kullanici.kullaniciAdi;
			kullaniciProfil.kullaniciSoyadi = kullanici.KullaniciSoyadi;
			kullaniciProfil.kullaniciEmail = kullanici.kullaniciEmail;
			kullaniciProfil.kullaniciDogum = kullanici.kullaniciDogum;
			kullaniciProfil.Phone = kullanici.Phone;
			

			return View(kullaniciProfil);
        }

        public IActionResult MyTickets(int Id)
        {
			
			if (Id != null)
            {
				List<TicketDB> tickets = _databaseContex.Ticket.Where(x=> x.kullaniciId==Id).ToList();
                List<TicketVM> model = new();

                foreach (TicketDB item in tickets)
                {
                    model.Add(new TicketVM
                    {
						havalimaniKalkis = item.havalimaniKalkis,
						havalimaniVaris = item.havalimaniVaris,
						sehirKalkis = item.sehirKalkis,
						sehirVaris = item.sehirVaris,
					    saatKalkis = item.saatKalkis,
					    saatVaris = item.saatVaris,
					    yolcuAdi = item.yolcuAdi,
					    yolcuSoyadi = item.yolcuSoyadi,
					    ucakModel = item.ucakModel,
					    koltukNo = item.koltukNo,
				    });
                }

				return View(model);
			}
            return NotFound("Bilet bulunamadı");
            
            
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
