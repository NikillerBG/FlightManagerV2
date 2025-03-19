using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using MVCApplication.Models;
using System.Diagnostics;

namespace MVCApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IdentityContext _identityContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IdentityContext identityContext)
        {
            _logger = logger;
            _context = context;
            _identityContext = identityContext;
        }

        public async Task<IActionResult> Index()
        {
            foreach (var role in Enum.GetNames<Role>())
            {
                if (!await _context.Roles.AnyAsync(x => x.Name == role))
                {
                    _context.Roles.Add(new IdentityRole(role) { NormalizedName = role.ToUpper() });
                }
            }

            await _context.SaveChangesAsync();

            if (await _identityContext.FindUserByNameAsync("admin@admin.net") is null)
            {
                await _identityContext.CreateUserAsync("admin@admin.net", "admin", Role.Admin);
            }
            return View();
        }

        public IActionResult Privacy()
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
