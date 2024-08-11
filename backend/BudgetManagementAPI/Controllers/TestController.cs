using Microsoft.AspNetCore.Mvc;

namespace BudgetManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMockData()
        {
            await Task.Delay(5000);
            return Ok(new {data= "data"});
        }
    }
}
