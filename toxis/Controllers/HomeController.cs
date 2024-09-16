using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Npgsql;
using System.Diagnostics;
using toxis.Models;

namespace toxis.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static string constring = "Host=localhost;Username=postgres;Password=VEST777berto;Database=postgres";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Инициализация модели и списка зон    
            var model = new client
            {
                Zones = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Танцпол", Text = "Танцпол" },
                    new SelectListItem { Value = "Нижний ярус", Text = "Нижний ярус" },
                    new SelectListItem { Value = "Средний ярус", Text = "Средний ярус" },
                    new SelectListItem { Value = "Балконы", Text = "Балконы" }
                }
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Check(client client)   
        {
            if (ModelState.IsValid)
            {
                var DataBase = new DataBase();
                var ticketId = await DataBase.addTicket(client);

                var zoneCost = await DataBase.zoneCost(client.SelectedZone);

                var ticketModel = new ticketModel
                {
                    ticketId = ticketId,
                    fullName = $"{client.firstName} {client.secondName} {client.surname}",
                    zoneCost = zoneCost
                };

                return View("TicketDetails", ticketModel);
            }

            client.Zones = new List<SelectListItem>
            {
                new SelectListItem { Value = "Танцпол", Text = "Танцпол" },
                new SelectListItem { Value = "Нижний ярус", Text = "Нижний ярус" },
                new SelectListItem { Value = "Средний ярус", Text = "Средний ярус" },
                new SelectListItem { Value = "Балконы", Text = "Балконы" }
            };

            return View("Index", client);
        }

        public IActionResult Success()
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
