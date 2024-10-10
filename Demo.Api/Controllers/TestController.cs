using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> TestSerilog()
        {
            var primoNumero = 1;
            var secondoNumero = 0;

            return Ok(primoNumero / secondoNumero);
        }
    }

}
