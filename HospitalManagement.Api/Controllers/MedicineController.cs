using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class MedicineController : ControllerBase
    {
        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}
        

        [HttpGet]
        public async Task<ActionResult> GetMedicine()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://myhealthbox.p.rapidapi.com/search/updatedDocuments?sd=2020-06-01&c=us&l=en"),
                Headers =
            {
                { "X-RapidAPI-Key", "d330c66931msh978bcef6ce16a4ap112624jsn4dcebc6b948a" },
                { "X-RapidAPI-Host", "myhealthbox.p.rapidapi.com" },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return Ok(body);
            }
        }
    }
}

