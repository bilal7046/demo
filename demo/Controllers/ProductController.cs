using demo.Models;
using demo.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IDataService _service;

        public ProductController(IDataService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            try
            {
                XDocument xDocument = XDocument.Parse(product.Xml);
                var status = _service.InsertProduct(product.Xml);

                if (status)
                {
                    return Ok("");
                }
            }
            catch (Exception ex)
            {
                //log exception
            }
            return BadRequest();
        }
    }
}