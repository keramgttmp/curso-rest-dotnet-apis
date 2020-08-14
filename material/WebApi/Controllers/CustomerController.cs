using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ApplicationSettings _settings;
        private readonly CustomerRepository _repository;

        public CustomerController(ILogger<CustomerController> logger,
                                  ApplicationSettings settings,
                                  CustomerRepository repository)
        {
            _logger = logger;
            _settings = settings;
            _repository = repository;
        }

        [HttpGet]
        [Produces(typeof(CustomerViewModel))]
        public ActionResult<IEnumerable<object>> Get()
        {
            var result = _repository.Get();

            _logger.LogInformation("Variable {0}", _settings.Variable);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public object GetById(int id)
        {
            var result = _repository.Get(id);

            if (result == null)
            {
                return NotFound(new { Message = "No se encuentra el elemento" });
            }
            return result;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ViewModels.CustomerViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Parámetros inválidos");
            }

            request.CustomerId = _repository.Save(request);

            return CreatedAtAction(nameof(GetById), new { Id = request.CustomerId }, request);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CustomerViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Parámetros inválidos");
            }

            var result = _repository.Update(id, request);

            return result ? (IActionResult)Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] CustomerViewModel request)
        {

            var result = _repository.Delete(id, request);

            return result ? (IActionResult)Ok() : NotFound();
        }
    }
}
