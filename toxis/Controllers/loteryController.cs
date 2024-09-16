using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using toxis.Models;

namespace toxis.Controllers
{
    public class loteryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> result()
        {
            string apikey = "C1aHPrModTuZbZODEhVA6LfZyREpgrIA82ACevagWbRH_140737489973364";

            var Random = new Random();
            int randomZone = Random.Next(1, 4);
            string zone = "";
            switch (randomZone)
            {
                case 1:
                    zone = "Танцпол";
                    break;
                case 2:
                    zone = "Нижний ярус";
                    break;
                case 3:
                    zone = "Средний ярус";
                    break;
                case 4:
                    zone = "Балконы";
                    break;
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"{apikey}");
                string requestUrl = "https://api.randogram.ru/generateNames?lang=ru&limit=1";
                string requestUrl2 = "https://api.randogram.ru/generateStrings?limit=1";

                string result = await MakeRequest(client, requestUrl);
                string[] parts = result.Split(' ');
                string email = await MakeRequest(client, requestUrl2);



                var clientModel = new client
                {
                    firstName = parts[1],
                    secondName = parts[0],
                    surname = parts[2],
                    email = email + "@mail.ru",
                    SelectedZone = zone
                };

                var DataBase = new DataBase();
                var ticketId = await DataBase.addTicket(clientModel);

                var tictetModel = new ticketModel
                {
                    ticketId = ticketId,
                    fullName = result,
                    zone = zone
                };

                return View(tictetModel);
            }
        }

        static async Task<string> MakeRequest(HttpClient client, string requestUrl)
        {
            
            HttpResponseMessage response = await client.GetAsync(requestUrl);
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"Ошибка: {response.StatusCode}";
            }
        }

    }
}
