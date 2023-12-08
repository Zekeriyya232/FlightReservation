﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NETCore.Encrypt.Extensions;
using WebProje2023.Entity;
using WebProje2023.Models;

namespace WebProje2023.Controllers
{
	[Authorize(Roles ="admin,yonetici")]
    public class AdminController : Controller
    {
        private readonly DatabaseContext _databaseContex;
        private readonly IConfiguration _configuration;

        public AdminController(DatabaseContext databaseContex, IConfiguration configuration)
        {
            _databaseContex = databaseContex;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult KullaniciGet()
        {
            List<KullaniciDB> kullanicilar = _databaseContex.Kullanici.ToList();
            List<KullaniciVM> model = new List<KullaniciVM>();

            // _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
            //   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

            foreach (KullaniciDB item in kullanicilar)
            {
                model.Add(new KullaniciVM
                {
                    Id = item.Id,
                    kullaniciAdi = item.kullaniciAdi,
                    KullaniciSoyadi = item.KullaniciSoyadi,
                    kullaniciEmail = item.kullaniciEmail,
                    Role = item.Role,
                    KayitTarih = item.KayitTarih,
                    Locked = item.Locked,
                    Phone = item.Phone,
                });
            }
            //admin sayfasında kullanıcıları listelemek için kullan 


            return View(model);
        }

        public IActionResult KullaniciCreate() { 
            return View();
        }

        [HttpPost]
        public IActionResult KullaniciCreate(SignUpVm signUpVm)
        {
			if (ModelState.IsValid)
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
				return View("KullaniciGet");
			}
			else
			{
				return View(signUpVm);
			}

		}

        public IActionResult KullaniciEdit(int Id)
        {
			KullaniciDB kullanici = _databaseContex.Kullanici.Find(Id);
			KullaniciEditVM editKullanici = new KullaniciEditVM();
			editKullanici.kullaniciAdi = kullanici.kullaniciAdi;
			editKullanici.kullaniciSoyadi = kullanici.KullaniciSoyadi;
			editKullanici.kullaniciEmail = kullanici.kullaniciEmail;
			editKullanici.kullaniciDogum = kullanici.kullaniciDogum;
			editKullanici.Locked = kullanici.Locked;
			editKullanici.Phone = kullanici.Phone;
			editKullanici.Role = kullanici.Role;


			return View(editKullanici);
        }

        [HttpPost]
		public IActionResult KullaniciEdit(int Id,KullaniciEditVM kullaniciEditVM)
		{
			if (ModelState.IsValid)
			{
				KullaniciDB kullanici = _databaseContex.Kullanici.Find(Id);
				kullanici.kullaniciAdi = kullaniciEditVM.kullaniciAdi;
				kullanici.KullaniciSoyadi = kullaniciEditVM.kullaniciSoyadi;
				kullanici.kullaniciEmail = kullaniciEditVM.kullaniciEmail;
				kullanici.kullaniciDogum = kullaniciEditVM.kullaniciDogum;
				kullanici.Locked = kullaniciEditVM.Locked;
				kullanici.Phone = kullaniciEditVM.Phone;
				kullanici.Role = kullaniciEditVM.Role;

				_databaseContex.SaveChanges();


				List<KullaniciDB> kullanicilar = _databaseContex.Kullanici.ToList();
				List<KullaniciVM> model2 = new List<KullaniciVM>();

				// _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
				//   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

				foreach (KullaniciDB item in kullanicilar)
				{
					model2.Add(new KullaniciVM
					{
						Id = item.Id,
						kullaniciAdi = item.kullaniciAdi,
						KullaniciSoyadi = item.KullaniciSoyadi,
						kullaniciEmail = item.kullaniciEmail,
						Role = item.Role,
						KayitTarih = item.KayitTarih,
						Locked = item.Locked,
					});
				}
				//admin sayfasında kullanıcıları listelemek için kullan 


				return View("KullaniciGet", model2);

			}
			return View(kullaniciEditVM);

		}


		public IActionResult KullaniciDelete(int Id)
        {
			KullaniciDB kullanici = _databaseContex.Kullanici.Find(Id);
			if (kullanici != null)
			{
				_databaseContex.Kullanici.Remove(kullanici);
				_databaseContex.SaveChanges();

				List<KullaniciDB> kullanicilar = _databaseContex.Kullanici.ToList();
				List<KullaniciVM> model2 = new List<KullaniciVM>();

				// _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
				//   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

				foreach (KullaniciDB item in kullanicilar)
				{
					model2.Add(new KullaniciVM
					{
						Id = item.Id,
						kullaniciAdi = item.kullaniciAdi,
						KullaniciSoyadi = item.KullaniciSoyadi,
						kullaniciEmail = item.kullaniciEmail,
						Role = item.Role,
						KayitTarih = item.KayitTarih,
						Locked = item.Locked,
					});
				}
				//admin sayfasında kullanıcıları listelemek için kullan 
				return View("KullaniciGet", model2);

			}
			return NotFound();
		}


        public IActionResult RouteGet()
        {
            List<RoutesDB> routes = _databaseContex.Routes.ToList();
            List<RoutesVM> model = new List<RoutesVM>();

          

            foreach (RoutesDB item in routes)
            {
                model.Add(new RoutesVM
                {
                    Id = item.Id,
                    havalimaniKalkis = item.havalimaniKalkis,
                    havalimaniVaris=item.havalimaniVaris,
                    sehirKalkis = item.sehirKalkis,
                    sehirVaris=item.sehirVaris,
                    biletFiyat = item.biletFiyat,
                    tarihKalkis = item.tarihKalkis,
                    ucakModel = item.ucakModel,
                });
            }
            //admin sayfasında rotaları listelemek için
            return View(model);
        }

        public IActionResult RouteEdit(int Id)
        {
			RoutesDB routes = _databaseContex.Routes.Find(Id);
			RoutesVM routesVM = new();

			routesVM.havalimaniKalkis = routes.havalimaniKalkis;
			routesVM.havalimaniVaris = routes.havalimaniVaris;
			routesVM.sehirKalkis= routes.sehirKalkis;
			routesVM.sehirVaris = routes.sehirVaris;
			routesVM.tarihKalkis= routes.tarihKalkis;
			routesVM.kalkisSaat= routes.kalkisSaat;
			routesVM.biletFiyat= routes.biletFiyat;
			routesVM.ucakModel= routes.ucakModel;
			routesVM.varisSaat= routes.varisSaat;

            return View(routesVM);
        }

		[HttpPost]
		public IActionResult RouteEdit(int Id,RoutesEditVM routesEditVM)
		{
			if(ModelState.IsValid)
			{
				RoutesDB routes = _databaseContex.Routes.Find(Id);
				routes.havalimaniKalkis=routesEditVM.havalimaniKalkis;
				routes.havalimaniVaris = routesEditVM.havalimaniVaris;
				routes.sehirKalkis = routesEditVM.sehirKalkis;
				routes.sehirVaris=routesEditVM.sehirVaris;
				routes.tarihKalkis = routesEditVM.tarihKalkis;
				routes.kalkisSaat=routesEditVM.kalkisSaat;
				routes.varisSaat = routesEditVM.varisSaat;
				routes.biletFiyat = routesEditVM.biletFiyat;
				routes.ucakModel = routesEditVM.ucakModel;

				_databaseContex.SaveChanges();

				List<RoutesDB> routeList = _databaseContex.Routes.ToList();
				List<RoutesVM> model2 = new List<RoutesVM>();

				foreach (RoutesDB item in routeList)
				{
					model2.Add(new RoutesVM
					{
						Id = item.Id,
						havalimaniKalkis=item.havalimaniKalkis,
						havalimaniVaris=item.havalimaniVaris,
						sehirKalkis=item.sehirKalkis,
						sehirVaris=item.sehirVaris,
						tarihKalkis=item.tarihKalkis,
						kalkisSaat=item.kalkisSaat,
						varisSaat=item.varisSaat,
						biletFiyat=item.biletFiyat,
						ucakModel=item.ucakModel,
					});
				}
				return View("RoutesGet", model2);
			}
			return View(routesEditVM);
		}


		public IActionResult RouteDelete(int Id)
        {
			RoutesDB routes = _databaseContex.Routes.Find(Id);
			if (routes != null)
			{
				_databaseContex.Routes.Remove(routes);
				_databaseContex.SaveChanges();

				List<RoutesDB> routeList = _databaseContex.Routes.ToList();
				List<RoutesVM> model2 = new List<RoutesVM>();

				// _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
				//   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

				foreach (RoutesDB item in routeList)
				{
					model2.Add(new RoutesVM
					{
						Id = item.Id,
						havalimaniKalkis = item.havalimaniKalkis,
						havalimaniVaris = item.havalimaniVaris,
						sehirKalkis = item.sehirKalkis,
						sehirVaris = item.sehirVaris,
						tarihKalkis = item.tarihKalkis,
						kalkisSaat = item.kalkisSaat,
						varisSaat = item.varisSaat,
						biletFiyat = item.biletFiyat,
						ucakModel = item.ucakModel,
					});
				}
				return View("RoutesGet", model2);

			}
			return NotFound();
		}

		public IActionResult RouteCreate()
		{
			return View();
		}

		[HttpPost]
		public IActionResult RouteCreate(RoutesVM route)
		{
			if (ModelState.IsValid)
			{
				

				RoutesDB routes= new()
				{
					
					havalimaniKalkis = route.havalimaniKalkis,
					havalimaniVaris = route.havalimaniVaris,
					sehirKalkis = route.sehirKalkis,
					sehirVaris = route.sehirVaris,
					tarihKalkis = route.tarihKalkis,
					kalkisSaat = route.kalkisSaat,
					varisSaat = route.varisSaat,
					biletFiyat = route.biletFiyat,
					ucakModel = route.ucakModel,
				};

				_databaseContex.Routes.Add(routes);
				_databaseContex.SaveChanges();
				return View("RoutesGet");
			}
			else
			{
				return View(route);
			}
		}

		public IActionResult PlaneGet()
		{
			List<PlaneDB> planes = new();
			List<PlaneVM> model = new();

			foreach (PlaneDB item in planes)
			{
				model.Add(new PlaneVM
				{
					Id = item.Id,
					ucakModel= item.ucakModel,
					koltukSayisi = item.koltukSayisi,
				});
			}
			//admin sayfasında rotaları listelemek için
			return View(model);
		}

		public IActionResult PlaneCreate()
		{
			return View();
		}

		[HttpPost]
		public IActionResult PlaneCreate(PlaneVM planeVM)
		{
			if (ModelState.IsValid)
			{
				PlaneDB plane = new()
				{
					ucakModel = planeVM.ucakModel,
					koltukSayisi = planeVM.koltukSayisi,
				};

				_databaseContex.Plane.Add(plane);
				_databaseContex.SaveChanges();
				return View("PlaneGet");
			}
			else
			{
				return View(planeVM);
			}

		}

		public IActionResult PlaneDelete(int Id)
		{
			PlaneDB plane =_databaseContex.Plane.Find(Id);

			if (plane != null)
			{
				_databaseContex.Plane.Remove(plane);
				_databaseContex.SaveChanges();

				List<PlaneDB> planeList= _databaseContex.Plane.ToList();
				List<PlaneVM> model = new();

				// _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
				//   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

				foreach (PlaneDB item in planeList)
				{
					model.Add(new PlaneVM
					{
						Id = item.Id,
						ucakModel = item.ucakModel,
						koltukSayisi=item.koltukSayisi,
					});
				}
				return View("PlaneGet", model);

			}
			return NotFound();
		}

		public IActionResult PlaneEdit(int Id)
		{
			PlaneDB plane = _databaseContex.Plane.Find(Id);
			PlaneEditVM planeVM = new() { 
				ucakModel=plane.ucakModel,
				koltukSayisi = plane.koltukSayisi,
			};

			return View(planeVM);
		}

		[HttpPost]
		public IActionResult PlaneEdit(int Id,PlaneEditVM planeEditVM)
		{
			if(ModelState.IsValid)
			{
				PlaneDB plane = _databaseContex.Plane.Find(Id);
				plane.ucakModel=planeEditVM.ucakModel;
				plane.koltukSayisi=planeEditVM.koltukSayisi;

				_databaseContex.SaveChanges();

				List<PlaneDB> planeList=_databaseContex.Plane.ToList();
				List<PlaneVM> planes = new List<PlaneVM>();
				foreach(PlaneDB item in planeList)
				{
					planes.Add(new PlaneVM
					{
						Id = item.Id,
						ucakModel = item.ucakModel,
						koltukSayisi=item.koltukSayisi,
					});
				}
				return View("PlaneGet", planes);

			}
			return View(planeEditVM);
		}

		public IActionResult TicketGet()
		{
			List<TicketDB> ticketList= _databaseContex.Ticket.ToList();
			List<TicketVM> tickets = new List<TicketVM>();

			// _databaseContex.Kullanici.Select(x=>new KullaniciVM { 
			//   Id=x.Id,kullaniciAdi=x.kullaniciAdi,KullaniciSoyadi=x.KullaniciSoyadi,kullaniciEmail=x.kullaniciEmail})

			foreach (TicketDB item in ticketList)
			{
				tickets.Add(new TicketVM
				{
					Id=item.Id,
					kullaniciId=item.kullaniciId,
					havalimaniKalkis=item.havalimaniKalkis,
					havalimaniVaris=item.havalimaniVaris,
					sehirKalkis=item.sehirKalkis,
					sehirVaris=item.sehirVaris,
					saatKalkis=item.saatKalkis,
					saatVaris=item.saatVaris,
					yolcuAdi=item.yolcuAdi,
					yolcuSoyadi=item.yolcuSoyadi,
					ucakModel=item.ucakModel,
					koltukNo=item.koltukNo,
				});
			}
			//admin sayfasında kullanıcıları listelemek için kullan 


			return View(tickets);

		}

		public IActionResult TicketCreate()
		{

			return View();
		}

		[HttpPost]
		public IActionResult TicketCreate(TicketVM ticket)
		{

			//kullanicinin Id sini nasıl alacağına karar ver
			 
			if(ModelState.IsValid)
			{
				TicketDB ticketDB = new()
				{
					kullaniciId = ticket.kullaniciId,
					havalimaniKalkis = ticket.havalimaniKalkis,
					havalimaniVaris = ticket.havalimaniVaris,
					sehirKalkis = ticket.sehirKalkis,
					sehirVaris = ticket.sehirVaris,
					saatKalkis = ticket.saatKalkis,
					saatVaris = ticket.saatVaris,
					yolcuAdi = ticket.yolcuAdi,
					yolcuSoyadi = ticket.yolcuSoyadi,
					ucakModel = ticket.ucakModel,
					koltukNo = ticket.koltukNo,
				};
				_databaseContex.Ticket.Add(ticketDB);
				_databaseContex.SaveChanges();
				return View("TicketGet");
			}
			else { return View(ticket); }
			
		}
		
		public IActionResult TicketEdit(int Id)
		{
			TicketDB ticket = _databaseContex.Ticket.Find(Id);
			TicketEditVM ticketEdit = new()
			{
				kullaniciId = ticket.kullaniciId,
				havalimaniKalkis = ticket.havalimaniKalkis,
				havalimaniVaris = ticket.havalimaniVaris,
				sehirKalkis = ticket.sehirKalkis,
				sehirVaris = ticket.sehirVaris,
				saatKalkis = ticket.saatKalkis,
				saatVaris = ticket.saatVaris,
				yolcuAdi = ticket.yolcuAdi,
				yolcuSoyadi = ticket.yolcuSoyadi,
				ucakModel = ticket.ucakModel,
				koltukNo = ticket.koltukNo,

			};
			return View(ticketEdit);
		}

		[HttpPost]
		public IActionResult TicketEdit(int Id, TicketEditVM ticket)
		{
			if(ModelState.IsValid)
			{
				TicketDB ticketDb = _databaseContex.Ticket.Find(Id);
				ticketDb.kullaniciId = ticket.kullaniciId;
				ticketDb.havalimaniKalkis = ticket.havalimaniKalkis;
				ticketDb.havalimaniVaris = ticket.havalimaniVaris;
				ticketDb.sehirKalkis = ticket.sehirKalkis;
				ticketDb.sehirVaris = ticket.sehirVaris;
				ticketDb.saatKalkis = ticket.saatKalkis;
				ticketDb.saatVaris = ticket.saatVaris;
				ticketDb.yolcuAdi = ticket.yolcuAdi;
				ticketDb.yolcuSoyadi = ticket.yolcuSoyadi;
				ticketDb.ucakModel = ticket.ucakModel;
				ticketDb.koltukNo = ticket.koltukNo;

				_databaseContex.SaveChanges();

				List<TicketDB> tickets= _databaseContex.Ticket.ToList();
				List<TicketVM> model = new List<TicketVM>();

				foreach(TicketDB item in tickets)
				{
					model.Add(new TicketVM
					{
						Id = item.Id,
						kullaniciId = item.kullaniciId,
						havalimaniKalkis=item.havalimaniKalkis,
						havalimaniVaris=item.havalimaniVaris,
						sehirKalkis=item.sehirKalkis,
						sehirVaris=item.sehirVaris,
						saatKalkis=item.saatKalkis,
						saatVaris=item.saatVaris,
						yolcuAdi=item.yolcuAdi,
						yolcuSoyadi=item.yolcuSoyadi,
						ucakModel=item.ucakModel,
						koltukNo=item.koltukNo,
					});
				}
				return View("TicketGet", model);
			}
			return View(ticket);
		}

		public IActionResult TicketDelete(int Id)
		{
			TicketDB ticket = _databaseContex.Ticket.Find(Id);

			if (ticket != null)
			{
				_databaseContex.Ticket.Remove(ticket);
				_databaseContex.SaveChanges();

				List<TicketDB> ticketList= _databaseContex.Ticket.ToList();
				List<TicketVM> ticketVM = new List<TicketVM>();

				foreach(TicketDB item in ticketList)
				{
					ticketVM.Add(new TicketVM
					{
						Id = item.Id,
						kullaniciId = item.kullaniciId,
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
				return View("TicketGet", ticketVM);
			}
			return NotFound();
		}
    }
}