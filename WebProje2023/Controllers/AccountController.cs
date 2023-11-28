using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using WebProje2023.Entity;
using WebProje2023.Models;

namespace WebProje2023.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContex;
        private readonly IConfiguration _configuration;

        public AccountController(DatabaseContext databaseContex, IConfiguration configuration)
        {
            _databaseContex = databaseContex;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            if(ModelState.IsValid)
            {
                string md5Crypto = _configuration.GetValue<string>("AppSettings:MD5Crypto");
                string cryptoPassword = loginVM.KullaniciSifre + md5Crypto;
                string hashedPassword = cryptoPassword.MD5();

                KullaniciDB Kullanici = _databaseContex.Kullanici.SingleOrDefault(x => x.kullaniciEmail.ToLower() == loginVM.kullaniciEmail && x.kullaniciSifre == hashedPassword);


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

        public IActionResult SignUp()
        {
            return View();
        }

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
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
