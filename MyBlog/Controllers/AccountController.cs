
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace MyBlog.Controllers
{
	public class AccountController : Controller
	{
		private readonly AppDbContext _context;

		public IActionResult Login()
		{
			return View();
		}

        public AccountController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]

		public async Task<IActionResult> Login([FromForm] Login data)
		{
			var userFromDb = _context.Users
				.FirstOrDefault(x=>x.Username == data.Username
					&& x.Password == data.Password);

			if (userFromDb == null)
			{
				@ViewBag.Error = "User not found";
				return View();
			}
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, data.Username),
				new Claim(ClaimTypes.Role, "Admin"),
			};

			var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

			var identity = new ClaimsIdentity(claims, scheme);

			await HttpContext.SignInAsync(scheme, new ClaimsPrincipal(identity));
			return RedirectToAction ("Index", "Home");
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Login");
		}


		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromForm] User data)
		{
				var user = new User
				{
					Name = data.Name,
					Username = data.Username,
					Email = data.Email,
					Password = data.Password,
					Alamat = data.Alamat,
					NoTelepon = data.NoTelepon,
					TanggalLahir = data.TanggalLahir,
					Role = "Admin"
				};

			if (!ModelState.IsValid)
			{
				return View();
			}
				_context.Users.Add(user);
				await _context.SaveChangesAsync();

				var claims = new List<Claim>()
				{
					new Claim(ClaimTypes.Name, data.Username),
					new Claim(ClaimTypes.Role, "Admin"),
				};
			

			var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
			var identity = new ClaimsIdentity(claims, scheme);

			await HttpContext.SignInAsync(scheme, new ClaimsPrincipal(identity));
			return RedirectToAction("Index", "Home");
		}
		
		public IActionResult Show()
		{
			var users = _context.Users.ToList();
			return View(users);
		}
        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
			var indoCulture = CultureInfo.GetCultureInfo("id-ID");
			user.FormatTanggalLahir = user.TanggalLahir.ToString("d MMMM yyyy",indoCulture);
            return View(user);
        }


    }
}

    

