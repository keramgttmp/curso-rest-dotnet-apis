using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            //contenido en System.Net.Http;
            //se usa using pq el objeto es de una vida muy corta
            //y una vez que se completa deshecharlo.
            using ( var client = new HttpClient () ) 
            {
                //indicado la uri 
                client.BaseAddress = new Uri("http://localhost:5000/api/"); /*dirección del web api*/

                //HttpStatusCode a = HttpStatusCode.OK; //200

                //hacemos un request
                var result = await client.GetStringAsync("products");

                //escribimos el resultado
                _logger.LogInformation("Response:{0}", result);

                //Deserialización (JSON -> Object)

                Product[] models = JsonSerializer.Deserialize<Product[]>(result);

                ViewData["Modesl"] = models;

                //1) Cambiar Id=>id en Producto

                //2) Mostrar los productos en el HTML/Razor
                return Page();
            }
        }
    }
}
