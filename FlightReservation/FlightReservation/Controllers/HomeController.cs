﻿using FlightReservation.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics;
using System.Net.Sockets;
using System.Security.Claims;
using WebProje2023.Entity;
using WebProje2023.Models;
using WebProje2023.Services;

namespace WebProje2023.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly DatabaseContext _databaseContex;
		private readonly IConfiguration _configuration;
		private readonly LanguageService _languageService;


		public HomeController(DatabaseContext databaseContex, IConfiguration configuration , ILogger<HomeController> logger,LanguageService languageService)
		{
			_databaseContex = databaseContex;
			_configuration = configuration;
			_logger = logger;
			_languageService = languageService;
		}

		public IActionResult ChangeLanguage(string culture)
		{
			Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
				{
					Expires = DateTimeOffset.UtcNow.AddYears(1)
				});
			return Redirect(Request.Headers["Referer"].ToString());
		}


		public IActionResult Index()
		{
			List<AirportDB> airports = _databaseContex.Airports.ToList();
		
            ViewBag.airportList = new List<string>();
			ViewBag.airportKisaltma=new List<string>();

            foreach (AirportDB item in airports)
			{
                ViewBag.airportList.Add(item.havalimaniAdi);
            }

            foreach (AirportDB item in airports)
			{
                ViewBag.airportKisaltma.Add(item.havalimaniKisaltma);
            }

            return View();
		}
		[HttpPost]
		public IActionResult Index(SelectRouteVM model)
		{

            List<AirportDB> airports = _databaseContex.Airports.ToList();

            ViewBag.airportList = new List<string>();
            ViewBag.airportKisaltma = new List<string>();

            foreach (AirportDB item in airports)
            {
                ViewBag.airportList.Add(item.havalimaniAdi);
            }

            foreach (AirportDB item in airports)
            {
                ViewBag.airportKisaltma.Add(item.havalimaniKisaltma);
            }

            if (ModelState.IsValid)
			{
				List<RoutesDB> routes = _databaseContex.Routes.Where(x=> x.havalimaniKalkis == model.havalimaniKalkis && x.havalimaniVaris == model.havalimaniVaris && x.tarihKalkis==model.tarih).ToList();

                if (routes.Any())
				{
					List<RoutesVM> routesVM = new List<RoutesVM>();
					foreach(RoutesDB item in routes) {
						routesVM.Add(new RoutesVM
						{
							Id = item.Id,
							havalimaniKalkis = item.havalimaniKalkis,
							havalimaniVaris = item.havalimaniVaris,
							sehirKalkis = item.sehirKalkis,
							sehirVaris = item.sehirVaris,
							tarihKalkis = item.tarihKalkis,
							kalkisSaat =item.kalkisSaat,
							varisSaat = item.varisSaat,
							biletFiyat = item.biletFiyat,
							ucakModel = item.ucakModel,
						});
					}
					return View("SelectTicket", routesVM);
				}
				return View(model);  // uçuş bulunamadı hatası yap
			}
			return View(model);
		}

		public IActionResult SelectTicket(List<RoutesVM> model)
		{
			return View(model);
		}

		
		public IActionResult SelectSeat(int Id)
		{
			RoutesDB route = _databaseContex.Routes.Find(Id);
			PlaneDB plane = _databaseContex.Plane.SingleOrDefault(x => x.ucakModel.ToUpper() ==route.ucakModel.ToUpper());
			ViewBag.koltukSayisi = plane.koltukSayisi;

			return View();

		 

		}
		[HttpPost]
		public IActionResult SelectSeat(int Id , string koltukNumarasi)
		{
			RoutesDB route = _databaseContex.Routes.Find(Id);
			string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
			int intId = Convert.ToInt32(id);
			KullaniciDB kullanici = _databaseContex.Kullanici.Find(intId);
			TicketDB ticket = new()
			{
				kullaniciId = kullanici.Id,
				havalimaniKalkis = route.havalimaniKalkis,
				havalimaniVaris = route.havalimaniVaris,
				sehirKalkis = route.sehirKalkis,
				sehirVaris = route.sehirVaris,
				saatKalkis = route.kalkisSaat,
				saatVaris = route.varisSaat,
				yolcuAdi = kullanici.kullaniciAdi,
				yolcuSoyadi = kullanici.KullaniciSoyadi,
				ucakModel = route.ucakModel,
				koltukNo = koltukNumarasi,
				tarihKalkis = route.tarihKalkis,
				biletFiyat = route.biletFiyat,
			};
			_databaseContex.Ticket.Add(ticket);
			_databaseContex.SaveChanges();

			TicketVM ticketVM = new()
			{
				kullaniciId = kullanici.Id,
				havalimaniKalkis = route.havalimaniKalkis,
				havalimaniVaris = route.havalimaniVaris,
				sehirKalkis = route.sehirKalkis,
				sehirVaris = route.sehirVaris,
				saatKalkis = route.kalkisSaat,
				saatVaris = route.varisSaat,
				yolcuAdi = kullanici.kullaniciAdi,
				yolcuSoyadi = kullanici.KullaniciSoyadi,
				ucakModel = route.ucakModel,
				koltukNo = koltukNumarasi,
				tarihKalkis=route.tarihKalkis,
				biletFiyat=route.biletFiyat,
			};

			return RedirectToAction("MyTickets","Account",kullanici.Id);
		}

		public IActionResult ShowTicket(TicketVM ticketVM) {

			return View(ticketVM);
		}


		public IActionResult AccessDenied()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}