using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Restfulangularcore.Configuration;
using Restfulangularcore.Models;

namespace Restfulangularcore.Controllers
{
    public class HomeController : Controller
    {
        readonly RecordModel _model;
        
        public HomeController(IOptions<CouchbaseSettings> settings)
        {
            _model = new RecordModel(settings.Value);
        }

        public IActionResult Index() {
            return View();
        }

        [Route("api/get")]
        public async Task<IActionResult> Get(Guid? document_id)
        {
            if (!document_id.HasValue)
                return BadRequest("A document id is required");

            return Ok(await _model.GetByDocumentId(document_id.Value));
        }

        [Route("api/getAll")]
        public async Task<dynamic> GetAll()
        {
            return Ok(await _model.GetAll());
        }

        [Route("api/save")]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Person body)
        {
            // validation
            if (string.IsNullOrEmpty(body.FirstName))
                return BadRequest("A firstname is required");
            if (string.IsNullOrEmpty(body.LastName))
                return BadRequest("A lastname is required");
            if (string.IsNullOrEmpty(body.Email))
                return BadRequest("An email is required");

            return Ok(await _model.Save(body));
        }

        [Route("api/delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (!id.HasValue)
                return BadRequest("A document id is required");

            return Ok(await _model.Delete(id.Value));
        }
    }
}
