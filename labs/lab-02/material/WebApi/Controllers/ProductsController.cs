using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //se agrega a la ruta api/ y se mantiene la palabra [controller]
    public class ProductsController : ControllerBase
    {
        private static readonly string[] Products = new[]
        {
            "Jeans", "T-Shirt", "Pants"
        };

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> Get()
        {
            int i = 0;
            var result = Products.Select(model => new
            {
                Name = model,
                Id = i++
            });

            return Ok(result);
        }

        [HttpGet("{id}")] //verbo con parámetro se invocaría así http://localhost:5000/api/products/0
        public object GetById( int id)
        {
            int i = 0;
            var result = Products.Select(model => new
            {
                Name = model,
                Id = i++
            }).ToList();

            if (result.ElementAtOrDefault(id) == null)
            {
                //return NotFound("No se encontró el elemento"); //se devuelve como un mensaje 404Not Found ver System.Net.HttpStatusCode

                return NotFound ( new
                {
                    Message= "No se encontró el elemento"
                });
            }

            return result[id];
        }
    }
}
